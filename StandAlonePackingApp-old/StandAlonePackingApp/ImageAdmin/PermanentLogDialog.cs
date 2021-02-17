using System;
using System.Windows.Forms;
using ShopFloorLib;

namespace StandAlonePackingApp
{
    public partial class PermanentLogDialog : Form
    {
        public PermanentLogDialog()
        {
            InitializeComponent();
        }

        private void PermanentLogDialog_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Created");
            listView1.Columns[0].Width = 160;

            listView1.Columns.Add("Message");
            listView1.Columns[1].Width = listView1.Width - 4 - listView1.Columns[0].Width - SystemInformation.VerticalScrollBarWidth;

            dateFrom.Value = DateTime.Now.AddDays(-7);
            dateTo.Value = DateTime.Now;

            UpdateList();
        }

        private void UpdateList()
        {
            if (dateFrom.Value > dateTo.Value)
            {
                MessageBox.Show("From Date cannot be after the To Date", "Invalid Date Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listView1.Items.Clear();
            var l = PermanentLog.GetLogEntries(dateFrom.Value, dateTo.Value);
            
            if(l==null)
                return;

            foreach( var logEntry in l)
            {
                var s = new string[2];
                s[0] = logEntry.created.ToString("dd/MM/yy hh:mm:ss tt");
                s[1] = logEntry.msg;
                listView1.Items.Add(new ListViewItem(s));
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateList();
        }
    }
}
