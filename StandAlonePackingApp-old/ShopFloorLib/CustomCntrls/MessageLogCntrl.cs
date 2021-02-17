using System.Drawing;
using System.Windows.Forms;

namespace ShopFloorLib
{
    public partial class MessageLogCntrl : UserControl
    {
        delegate void AppendMsgCallback(string msg, Color col);

        private int msgNum;

        public MessageLogCntrl()
        {
            InitializeComponent();
            listView1.Columns.Add("col1", -2);
        }
        public void Add(string msg, MessageLogger.MsgLevel lvl)
        {
            MessageBoxIcon icon;
            Color col;

            switch (lvl)
            {
                case MessageLogger.MsgLevel.critical: icon = MessageBoxIcon.Exclamation; col = Color.Tomato; break;
                case MessageLogger.MsgLevel.error: icon = MessageBoxIcon.Error; col = Color.Tomato; break;
                case MessageLogger.MsgLevel.warning: icon = MessageBoxIcon.Warning; col = Color.PaleGoldenrod; break;
                case MessageLogger.MsgLevel.info: icon = MessageBoxIcon.Information; col = Color.LightGreen; break;
                default: icon = MessageBoxIcon.None; col = Color.Green; break;
            }
            msgNum++;
            AppendMsg(msgNum + ". " + msg, col);

            if (lvl == MessageLogger.MsgLevel.critical)
            {
                MessageBox.Show(msg, "Message", MessageBoxButtons.OK, icon);
                Application.Exit();
            }
        }
        private void AppendMsg(string msg, Color col)
        {
            if (listView1.InvokeRequired)
            {
                var d = new AppendMsgCallback(AppendMsg);
                this.Invoke(d, new object[] { msg, col });
            }
            else
            {
                var itm = new ListViewItem(msg);
                itm.BackColor = col;
                listView1.Items.Add(itm);

                if (listView1.Items.Count > 200)
                    //Only keep the last 200 messages in the ListView, the rest can be seen in the .txt log file.
                    listView1.Items.RemoveAt(0);

                // Scroll to the bottom of the list - i.e. always show the bottom (newest) message
                listView1.Items[listView1.Items.Count - 1].EnsureVisible();

                // Set the width of the single colunm to the biggest it can be without causing a horizontal scrollbar to appear
                listView1.Columns[0].Width = Width - 4 - SystemInformation.VerticalScrollBarWidth;
            }
        }
    }
}
