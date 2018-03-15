using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace ComSerie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getPort();
        }
        void getPort()
        {
            string[] ports = SerialPort.GetPortNames();
            CbAfficheur.Items.AddRange(ports);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // ici on va gérer les exception pour la securité du programme
            try
            {
                if(CbAfficheur.Text == "" || cboBaudRate.Text =="" /* || cbParity.Text=="" 
                    ||cbDatabits.Text==""  ||cbstopbits.Text==""*/)
                {
                   
                    MessageBox.Show("AUCUN PARAMETRE SELECTIONNER !!!");
                }
                else
                {

                    // permet d'afficher la valeur du port lu
                    serialPort1.PortName = CbAfficheur.Text;
                    // permet de convertir la valeur du baud en int
                    serialPort1.BaudRate = Convert.ToInt32((cboBaudRate.Text));
                    serialPort1.Parity   = (Parity)Enum.Parse(typeof(Parity), cbParity.Text);
                    serialPort1.DataBits = Convert.ToInt32(cbDatabits.Text);
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbstopbits.Text);
                    //permet d'ouvrir le port selecionné
                    serialPort1.Open();
                    // bar je chargement d'envoie des données
                    progressBar1.Value = 100;
                    //if (progressBar1.Value == 100)
                    //    MessageBox.Show("PORT OUVERT !!");
                    //cbParity.Enabled = true;
                    //cbDatabits.Enabled = true;
                    //cbstopbits.Enabled = true;
                    btnenvoi.Enabled = true;
                    btnlecture.Enabled = true;
                    tbSendData.Enabled = true;
                    btnClose.Enabled = true;
                    btnOpen.Enabled = false;


                }
            }
            catch(UnauthorizedAccessException)
            {
                tbreception.Text = "ACCES NON AUTORISE !";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            progressBar1.Value = 0;
            btnenvoi.Enabled = false;
            btnlecture.Enabled = false;
            btnOpen.Enabled = false;
            btnClose.Enabled = false;
            tbSendData.Enabled = true;
        }

        private void btnenvoi_Click(object sender, EventArgs e)
        {
            if (tbSendData.Text == "")
            {
                MessageBox.Show("AUCUN VALEUR ENTRER !!!");
            }
            serialPort1.WriteLine(tbSendData.Text);
            //tbSendData.Text = "";
        }

        private void btnlecture_Click(object sender, EventArgs e)
        {
            try
            {
                tbreception.Text = tbSendData.Text;
            }
            catch(TimeoutException)
            {
                tbreception.Text = "Time out Exception";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            CbAfficheur.Items.Clear();
            cboBaudRate.Items.Clear();
            tbSendData.Text  = "";
            tbreception.Text = "";
            getPort();
            cboBaudRate.Items.Add(9600);
            btnenvoi.Enabled = false;
            btnlecture.Enabled = false;
            tbSendData.Enabled = false;
            btnClose.Enabled = false;
            btnOpen.Enabled = true;
        }
    }
}