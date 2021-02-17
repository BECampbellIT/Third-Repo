using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 
 * ButtonMatrix user control provides a generic n x m matrix with buttons displaying text.
 * */
namespace ShopFloorLib
{
    public partial class ButtonMatrix : UserControl
    {
        /* Classes to be displayed within a ButtonMatrix must implement this interface */
        public interface MatrixObject
        {
            string GetKey();
            string ToString();
            bool MatchesFilter();
            Color GetNormalColor();
            Color GetSelectedColor();
        }

        #region Private Attributes
        private int numRows;
        private int numCols;
        private int numButtons;
        private List<MatrixObject> myList;
        private List<Button> myButtons;
        private List<MatrixObject> displayedObjs;
        private MatrixObject selectedObj = null;
        private bool autoDeselect = false;

        private int idxFirstButtonCurrPage = 0;
        private int idxFirstButtonNextPage;

        delegate void UpdateButtonTextCallback(Button btn, string text);
        #endregion

        #region Constructors
        public ButtonMatrix()
        {
            InitializeComponent();
        }
        #endregion

        #region ButtonSelected event
        public event EventHandler<ButtonSelectedEventArgs> ButtonSelected;

        protected virtual void OnButtonSelected(ButtonSelectedEventArgs e)
        {
            EventHandler<ButtonSelectedEventArgs> handler = ButtonSelected;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class ButtonSelectedEventArgs : EventArgs
        {
            public ButtonMatrix.MatrixObject obj { get; set; }
        }
        #endregion

        #region Setup
        /*
         * Sets up the button matrix and data source. Should be called from parent Form's constructor
         */
        public void Setup(int _numRows, int _numCols, List<ButtonMatrix.MatrixObject> _myList, ButtonMatrix.MatrixObject _selectedObj = null)
        {
            //Set instance attributes
            numRows = _numRows;
            numCols = _numCols;
            numButtons = numRows * numCols;
            myButtons = new List<Button>();

            //Firstly remove default row and column from this control and add new ones of the correct % for
            //the numCols x numRows matrix
            float rowPerc = 100 / numRows;
            float colPerc = 100 / numCols;

            tabLayoutInside.RowStyles.Clear();
            tabLayoutInside.RowCount = numRows;

            tabLayoutInside.ColumnStyles.Clear();
            tabLayoutInside.ColumnCount = numCols;

            for (int i = 0; i < numRows; i++)
            {
                tabLayoutInside.RowStyles.Add(new RowStyle(SizeType.Percent, rowPerc));
            }
            for (int i = 0; i < numCols; i++)
            {
                tabLayoutInside.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colPerc));
            }
            //Add a Button control in each cell of the matrix
            for (int i = 0; i < numButtons; i++)
            {
                Button b = GetNewButton();
                myButtons.Add(b);
                tabLayoutInside.Controls.Add(b);
            }
            SetList(_myList, _selectedObj);
        }
        #endregion

        #region SetList
        public void SetList(List<ButtonMatrix.MatrixObject> _myList, ButtonMatrix.MatrixObject _selectedObj = null)
        {
            myList = _myList;
            selectedObj = _selectedObj;
            idxFirstButtonCurrPage = 0;

            if (myList == null)
            {
                this.Visible = false;
                return;
            }
            autoDeselect = selectedObj == null;
            this.Visible = true;
            RefreshButtonMatrix();
        }
        #endregion

        #region GetNewButton
        /*
         * Default properties of the buttons. They fill their cells with a small margin between each.
         */
        private Button GetNewButton()
        {
            Button b = new Button();
            b.TextAlign = ContentAlignment.MiddleCenter;
            b.AutoSize = true;
            b.Visible = false;
            b.Dock = DockStyle.Fill;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 2;
            b.FlatAppearance.MouseDownBackColor = Color.White;
            b.Click += new System.EventHandler(this.btnMatrix_Click);
            return b;
        }
        #endregion

        #region Redraw
        /*
         * Goes back to the top of the first page and redraws the buttons
         */
        public void Redraw()
        {
            idxFirstButtonCurrPage = 0;
            RefreshButtonMatrix();
        }
        #endregion

        #region UpdateButton
        public void UpdateButton(string key)
        {
            int idx = displayedObjs.FindIndex(o => o.GetKey().Equals(key));
            if (idx >= 0)
            {
                UpdateButtonText(myButtons[idx], displayedObjs[idx].ToString());
            }
        }
        #endregion

        #region UpdateButtonText
        private void UpdateButtonText(Button btn, string text)
        {
            if (btn.InvokeRequired)
            {
                UpdateButtonTextCallback u = new UpdateButtonTextCallback(UpdateButtonText);
                this.Invoke(u, new Object[] { btn, text });
            }
            else
                btn.Text = text;
        }
        #endregion

        #region RefreshButtonMatrix
        private void RefreshButtonMatrix()
        {
            int numberOfButtons = 0;

            idxFirstButtonNextPage = 0;
            displayedObjs = new List<MatrixObject>();

            if (autoDeselect)
            { 
                // Effectively de-select current selection   
                selectedObj = null;
            }

            for(int i=idxFirstButtonCurrPage; i<myList.Count; i++)
            {
                if (!myList[i].MatchesFilter()) continue;

                if (numberOfButtons >= numButtons)
                {
                    idxFirstButtonNextPage = i;
                    break;
                }
                myButtons[numberOfButtons].Text = myList[i].ToString();
                myButtons[numberOfButtons].Visible = true;

                Color col;
                if (myList[i] == selectedObj)
                    col = myList[i].GetSelectedColor();
                else
                    col = myList[i].GetNormalColor();
                SetButtonColor(myButtons[numberOfButtons], col);

                displayedObjs.Add(myList[i]);
                numberOfButtons++;
            }
            for (int i = numberOfButtons; i < numButtons; i++)
            {
                myButtons[i].Visible = false;
            }

            // Set visibility of Page Up and Down buttons depending on whether there's next/previous pages to display
            btnPageDown.Visible = idxFirstButtonNextPage > 0;
            btnPageUp.Visible = idxFirstButtonCurrPage > 0;

            var args = new ButtonSelectedEventArgs();
            if (numberOfButtons == 1)
            {
                //Automatically select a button if it's the only one displayed
                selectedObj = displayedObjs[0];
                SetButtonColor(myButtons[0], selectedObj.GetSelectedColor());
                args.obj = selectedObj;
                OnButtonSelected(args);
            }
        }
        #endregion

        #region btnMatrix_Click
        /*
         * Respond to one of the matrix buttons being clicked.
         */
        private void btnMatrix_Click(object sender, EventArgs e)
        {
            if (selectedObj != null)
            {
                // Set the colour of the currently selected button back to normal.
                int idx = displayedObjs.FindIndex(o => o == selectedObj);
                if (idx != -1)
                    SetButtonColor(myButtons[idx], selectedObj.GetNormalColor());
            }

            // Set the colour of the newly selected button to its highlighted colour.
            int idxButtonSelected = myButtons.IndexOf((Button)sender);
            selectedObj = displayedObjs[idxButtonSelected];
            SetButtonColor(myButtons[idxButtonSelected], selectedObj.GetSelectedColor());

            // Raise ButtonSelected event
            var args = new ButtonSelectedEventArgs();
            args.obj = selectedObj;
            OnButtonSelected(args);
        }
        #endregion

        #region btnPageUp_Click
        /* 
         * Responds to clicking Page Up. It moves idxFirstButtonCurrPage back
         * through myList until it's at the top of the previous page. Then calls
         * RefreshButtonMatrix() to re-draw the screen.
         */
        private void btnPageUp_Click(object sender, EventArgs e)
        {
            int numMatching = 0;

            do
            {
                idxFirstButtonCurrPage--;
                if (myList[idxFirstButtonCurrPage].MatchesFilter())
                    numMatching++;
            } while (numMatching < numButtons && idxFirstButtonCurrPage > 0);

            RefreshButtonMatrix();
        }
        #endregion

        #region btnPageDown_Click
        /*
         * Responds to Page Down. Moves idxFirstButtonCurrPage to the first
         * object in myList that matches the filter after currently displayed buttons.
         * Then calls RefreshButtonMatrix() to re-draw the screen.
         */

        private void btnPageDown_Click(object sender, EventArgs e)
        {
            idxFirstButtonCurrPage = idxFirstButtonNextPage;
            RefreshButtonMatrix();
        }
        #endregion

        #region SetButtonColor
        private void SetButtonColor(Button b, Color c)
        {
            b.BackColor = c;
            b.FlatAppearance.MouseOverBackColor = c;
        }
        #endregion
    }
}
