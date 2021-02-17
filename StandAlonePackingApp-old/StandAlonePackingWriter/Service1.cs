using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StandAlonePackingLib;
using System.IO;
using ShopFloorLib;

namespace StandAlonePackingWriter
{
    public partial class Service1 : ServiceBase
    {
        private bool stopping;

        public Service1()
        {
            InitializeComponent();
            this.stopping = false;
        }

        protected override void OnStart(string[] args)
        {
            // Set the current working directory to the where the .exe file for the service resides
            var cwd = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            Directory.SetCurrentDirectory(cwd ?? ".");

            MessageLogger.Add("Starting Service", MessageLogger.MsgLevel.info);

            CommonData.Initialise();

            ThreadPool.QueueUserWorkItem(new WaitCallback(ServiceWorkerThread), null);    
        }

        protected override void OnStop()
        {
            this.stopping = true;
            MessageLogger.Add("Stopping Service...", MessageLogger.MsgLevel.info);
        }

        private void ServiceWorkerThread(object state)
        {
            int matTurns;  // Number of times the Send Cartons interval occurs for each Material Read interval
            int orderTurns; // Number of times the Send Cartons interval occurs for each Order Read interval

            int matTurnsToGo = 0;  // Start with zero turns to go i.e. pull orders and material from SAP when service is first started.
            int orderTurnsToGo = 0;

            bool initilised = false;

            if (CommonData.localSettings.MaterialReadInterval != 0)
                matTurns = CommonData.localSettings.MaterialReadInterval * 60 * 60 / CommonData.localSettings.CartonSendInterval;
            else
                matTurns = -1;

            if (CommonData.localSettings.OrderReadInterval != 0)
                orderTurns = CommonData.localSettings.OrderReadInterval * 60 / CommonData.localSettings.CartonSendInterval;
            else
                orderTurns = -1;
           
            if( matTurns == -1 && orderTurns != -1)
            {
                // We're pulling Prod Orders from SAP but not Materials, read in the current material master records from local DB
                MessageLogger.Add("Reading materials from local DB", MessageLogger.MsgLevel.info);
                CommonData.mats = Material.readMaterialsFromDB();
            }
            while (!this.stopping)
            {
                if (!initilised)
                {
                    // Initialise DB connection
                    initilised = Order.Initialise();
                }

                if (initilised)
                {
                    // Send new cartons to SAP 
                    MessageLogger.Add("Sending Packs to SAP", MessageLogger.MsgLevel.info);
                    bool packsSentOk = Pack.SendPacks();

                    if (matTurns != -1)
                    {
                        if (matTurnsToGo == 0)
                        {
                            // It's time to read Materials from SAP
                            MessageLogger.Add("Reading Materials from SAP", MessageLogger.MsgLevel.info);
                            if (Material.readMaterialsFromSAP(out CommonData.mats))
                            {
                                MessageLogger.Add("Saving Materials to local DB", MessageLogger.MsgLevel.info);
                                Material.saveMaterialsToDB(CommonData.mats, true);
                            }
                            matTurnsToGo = matTurns;
                        }
                        else
                            matTurnsToGo--;
                    }

                    if (orderTurns != -1)
                    {
                        if (orderTurnsToGo == 0 && packsSentOk)
                        {
                            // It's time to read Production Orders from SAP
                            MessageLogger.Add("Reading Production Orders from SAP", MessageLogger.MsgLevel.info);
                            if (Order.ReadOrdersFromSAP(out CommonData.normalOrders, out CommonData.reworkOrders, out CommonData.slDates))
                            {
                                MessageLogger.Add("Saving Production Orders to local DB", MessageLogger.MsgLevel.info);
                                Order.SaveOrdersToDB(CommonData.normalOrders);
                                Order.SaveOrdersToDB(CommonData.reworkOrders);
                                Order.SavePackDatesToDB(CommonData.slDates);
                            }
                            orderTurnsToGo = orderTurns;
                        }
                        else
                            orderTurnsToGo--;
                    }
                }
                // Go to sleep
                Thread.Sleep(TimeSpan.FromSeconds(CommonData.localSettings.CartonSendInterval));  
            }
        }
    }
}
