using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ShopFloorLib
{
    public partial class ScaleIndicator: UserControl
    {
        private decimal tareWeight;
        private decimal grossWeight;
        private decimal netWeight;
        private ScaleReader.Stability stab;
        private ScaleReader sr;

        delegate void UpdateComponentsCallback();

        public ScaleIndicator()
        {
            InitializeComponent();

            netWeight = 0;
            grossWeight = 0;
            tareWeight = 0;
            stab = ScaleReader.Stability.unknown;
        }

        public void SetReader(ScaleReader _sr)
        {
            sr = _sr;
            sr.Open();
            backgroundWorker1.RunWorkerAsync();
        }
        public ScaleReader.Stability ReadScale(out decimal _grossWeight, out decimal _netWeight, out decimal _tareWeight)
        {
            _grossWeight = grossWeight;
            _netWeight = netWeight;
            _tareWeight = tareWeight;
            return stab;
        }
        public void SetTare(decimal newTare)
        {
            if (sr.settings.localTare)
                tareWeight = newTare;
            else
                sr.SetTare(newTare);
        }
        private void UpdateComponents()
        {
            if (lblNet.InvokeRequired)
            {
                UpdateComponentsCallback d = new UpdateComponentsCallback(UpdateComponents);
                this.Invoke(d, new object[] {  });
            }
            else
            {
                lblNet.Text = string.Format("Net: {0} kg", netWeight.ToString("0.00"));
                lblTare.Text = string.Format("Tare: {0} kg", tareWeight.ToString("0.000"));
                lblGross.Text = string.Format("Gross: {0} kg", grossWeight.ToString("0.00"));

                switch (stab)
                {
                    case ScaleReader.Stability.stableWeight:
                        BackColor = Color.Green; break;

                    case ScaleReader.Stability.unstableWeight:
                        BackColor = Color.Red; break;

                    default:
                        BackColor = Color.Yellow; break;
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (sr == null)
                return;

            while (true)
            {
                string res = sr.PollScaleWeight();
                if (res != null)
                {
                    if (sr.settings.localTare)
                    {
                        decimal dummyNet;
                        decimal dummyTare;

                        sr.InterpretResult(res, out grossWeight, out dummyNet, out dummyTare, out stab);
                        netWeight = grossWeight - tareWeight;
                    }
                    else
                        sr.InterpretResult(res, out grossWeight, out netWeight, out tareWeight, out stab);
                }
                else
                {
                    grossWeight = netWeight = tareWeight = 0;
                    stab = ScaleReader.Stability.unknown;
                }
                UpdateComponents();
                Thread.Sleep(200);
            }
        }
    }
}
