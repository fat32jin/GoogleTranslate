using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace fanyi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //加载句型集合列表
            LoadJuxing();
            BindJuxingEditList();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }        

        #region 翻译
        private void btnMSZ2E_Click(object sender, EventArgs e)
        {
            ITranFac fac = new MsFac();
            string re = "";          

            if (CommonFun.HasChinese(txtOrign.Text))
            {
                re = fac.TranTxt(txtOrign.Text, "zh-CHS", "en");
            }
            else
            {
                re = fac.TranTxt(txtOrign.Text, "en", "zh-CHS");
            }
                txtBing.Text = re;

        }       

      

        private void btnGoogle_Click(object sender, EventArgs e)
        {
             ITranFac fac = new GoogleDotNetFac();
            if (CommonFun.HasChinese(txtOrign.Text))
            {
                txtGoogle.Text = fac.TranTxt(txtOrign.Text.Trim(), "zh-CN", "en");
            }
            else
            {
                txtGoogle.Text = fac.TranTxt(txtOrign.Text.Trim(), "en", "zh-CN");
            }

            
        }

        private void btnAutoTranBaidu_Click(object sender, EventArgs e)
        {
            ITranFac fac = new BaiDuFac();
            string re = "";


            if (CommonFun.HasChinese(txtOrign.Text))
            {
                re = fac.TranTxt(txtOrign.Text, "zh-CHS", "en");
            }
            else
            {
                re = fac.TranTxt(txtOrign.Text, "en", "zh");
            }
            txtBaidu.Text = re;
        }

        private void btnAutoTranYouDao_Click(object sender, EventArgs e)
        {
            ITranFac fac = new YouDaoFac();
            string re = "";


            if (CommonFun.HasChinese(txtOrign.Text))
            {
                re = fac.TranTxt(txtOrign.Text, "auto", "EN");
            }
            else
            {
                re = fac.TranTxt(txtOrign.Text, "auto", "zh-CHS");
            }
            txtYoudao.Text = re;
        }

        #endregion

        #region 添加正文
        private void addGoogle_Click(object sender, EventArgs e)
        {
            txtAfterTran.Text += txtGoogle.Text + "\r\n\r\n";

        }

        private void addBing_Click(object sender, EventArgs e)
        {
            txtAfterTran.Text += txtBing.Text + "\r\n\r\n";
        }

        private void addYoudao_Click(object sender, EventArgs e)
        {
            txtAfterTran.Text += txtYoudao.Text + "\r\n\r\n";
        }

        private void addBaidu_Click(object sender, EventArgs e)
        {
            txtAfterTran.Text += txtBaidu.Text + "\r\n\r\n";
        }
        #endregion 


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
        #region 清空

        private void btnClsZW_Click(object sender, EventArgs e)
        {
            txtAfterTran.Text = "";
        }

        private void btnClsGoogle_Click(object sender, EventArgs e)
        {
            txtGoogle.Clear();
        }

        private void btnClsMs_Click(object sender, EventArgs e)
        {
            txtBing.Clear();
        }

        private void btnClsYouDao_Click(object sender, EventArgs e)
        {
            txtYoudao.Clear();
        }

        private void btnClsBaidu_Click(object sender, EventArgs e)
        {
            txtBaidu.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.txtOrign.Text = "";
        }
        #endregion




        //保存文件
        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "";
            sfd.InitialDirectory = @"C:\";
            sfd.Filter = "文本文件| *.txt";
            sfd.ShowDialog();

            string path = sfd.FileName;
            if (path == "")
            {
                return;
            }

            using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] buffer = Encoding.Default.GetBytes(txtAfterTran.Text);
                fsWrite.Write(buffer, 0, buffer.Length);
                MessageBox.Show("保存成功");

            }
        }




        #region 句型切换选择
        //句型双击
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtAfterTran.Text += this.juxingcollist.Text; //this.juxingcollist.SelectedItem.;
        }       

        //句型集合下拉
        private void cmbJuxingJiheList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbJuxingJiheList.SelectedValue != null&&
                !string.IsNullOrEmpty(this.cmbJuxingJiheList.SelectedValue.ToString()))
            {

                LoadJuxingCol(this.cmbJuxingJiheList.SelectedValue.ToString());
            }
        }

        //加载句型明细
        private void LoadJuxingCol(string colid)
        {
            this.juxingcollist.DataSource = null;
            this.juxingcollist.Items.Clear();
            
            
            DataTable juxingcolList = JuxingHelper.LoadJuxingColList(colid);

            this.juxingcollist.DisplayMember = "det_memo";
            this.juxingcollist.ValueMember = "det_id";
            this.juxingcollist.DataSource = juxingcolList;
        }

        //加载句型集合
        private void LoadJuxing()
        {
            this.cmbJuxingJiheList.DataSource = null;
            this.cmbJuxingJiheList.Items.Clear();

           DataTable juxingList = JuxingHelper.LoadJuxingList();
           
           
            this.cmbJuxingJiheList.DisplayMember = "col_name";
            this.cmbJuxingJiheList.ValueMember = "col_id";

            this.cmbJuxingJiheList.DataSource = juxingList;


        }

        //加载句型集合
        private void BindJuxingEditList()
        {
            this.lstJuxingEdit.DataSource = null;
            this.lstJuxingEdit.Items.Clear();

            DataTable juxingList = JuxingHelper.LoadJuxingColList("1");


            this.lstJuxingEdit.DisplayMember = "det_memo";
            this.lstJuxingEdit.ValueMember = "det_id";
            this.lstJuxingEdit.DataSource = juxingList;


        }



        #endregion

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        
        private void btnAddJuXingName_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtInputJuXingDetail.Text.Trim()))
            {
                JuxingHelper.AddJuXing(this.txtInputJuXingDetail.Text.Trim());
                BindJuxingEditList();
                LoadJuxing();
                this.txtInputJuXingDetail.Text = "";
            }
        }

        private void btnDelJuXingName_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.lstJuxingEdit.SelectedValue.ToString()))
            {

                //delete mingxi
                JuxingHelper.DelJuXing(this.lstJuxingEdit.SelectedValue.ToString());
                BindJuxingEditList();
                LoadJuxing();


            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void lstJuxingEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.label5.Text = this.lstJuxingEdit.Text;
        }

        private void juxingcollist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.label6.Text = this.juxingcollist.Text;
        }
    }
}
