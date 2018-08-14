using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHouseKeepingBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddData();
        }

        private void AddData()
        {
            ItemForm itemForm = new ItemForm(categoryDataSet1);
            DialogResult drRet = itemForm.ShowDialog();

            if (drRet == DialogResult.OK)
            {
                moneyDataSet.moneyDataTable.AddmoneyDataTableRow(
                    itemForm.monCalender.SelectionRange.Start
                    , itemForm.cmbCategory.Text
                    , itemForm.txtItem.Text
                    , int.Parse(itemForm.mtxtMoney.Text)
                    , itemForm.textRemarks.Text);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            categoryDataSet1.CategoryDataTable.AddCategoryDataTableRow("給料", "入金");
            categoryDataSet1.CategoryDataTable.AddCategoryDataTableRow("食費", "出金");
            categoryDataSet1.CategoryDataTable.AddCategoryDataTableRow("雑費", "出金");
            categoryDataSet1.CategoryDataTable.AddCategoryDataTableRow("住居", "出金");

        }

        private void 追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddData();
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveData()
        {
            string path = "MoneyData.csv";
            string strData = "";

            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                path, false, System.Text.Encoding.Default);

            foreach (MoneyDataSet.moneyDataTableRow drMoney in moneyDataSet.moneyDataTable)
            {
                strData = drMoney.日付.ToShortDateString() + ","
                    + drMoney.分類 + ","
                    + drMoney.品名 + ","
                    + drMoney.金額.ToString() + ","
                    + drMoney.備考;

                sw.WriteLine(strData);
            }

            sw.Close();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveData();
        }

        private void LoadData()
        {
            string path = "MoneyData.csv";
            string delimStr = ",";
            char[] delimiter = delimStr.ToCharArray();
            string[] strData;
            string strLine;

            bool fileExist = System.IO.File.Exists(path);

            if (fileExist)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.Default);


                while (sr.Peek() >= 0)
                {
                    strLine = sr.ReadLine();
                    strData = strLine.Split(delimiter);

                    moneyDataSet.moneyDataTable.AddmoneyDataTableRow(
                        DateTime.Parse(strData[0]),
                        strData[1],
                        strData[2],
                        int.Parse(strData[3]),
                        strData[4]
                        );
                }

                sr.Close();
            }



        }
    }
}
