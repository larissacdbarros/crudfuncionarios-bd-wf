using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercicio01AulaBD_WF
{
    public partial class Form1 : Form
    {
        public bool selecao;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string connection = @"Data Source = DESKTOP-71RT81O\SQLEXPRESS; Initial Catalog = bd_revisao; Integrated Security = True";

            SqlConnection sqlConnection = new SqlConnection(connection);

            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand("Select * from funcionario", sqlConnection);

            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].TableName;

            sqlConnection.Close();

         

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtCPF.Text == "" || txtCargo.Text == "" || txtIdade.Text == "")
            {
                MessageBox.Show("Preencher todos os campos para realizar o cadastro.");
            }
            else
            {

                string connection = @"Data Source = DESKTOP-71RT81O\SQLEXPRESS; Initial Catalog = bd_revisao; Integrated Security = True";
                SqlConnection sqlConnection = new SqlConnection(connection);

                string sql = @"Insert into funcionario (Nome, CPF, Cargo, Idade) 
                        values ('" + txtNome.Text + "', '" + txtCPF.Text + "', '" + txtCargo.Text + "', '" + txtIdade.Text + "')";

                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.CommandType = CommandType.Text;

                sqlConnection.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Dado cadastrado com sucesso");
                        LimpaCampos();
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cadastro não realizado" + ex.Message);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtCPF.Text == "" || txtCargo.Text == "" || txtIdade.Text == "")
            {
                MessageBox.Show("Preencher todos os campos para realizar o cadastro.");
            }
            else
            {


                string connection = @"Data Source = DESKTOP-71RT81O\SQLEXPRESS; Initial Catalog = bd_revisao; Integrated Security = True";
                SqlConnection sqlConnection = new SqlConnection(connection);

                string sql = @"update funcionario set nome = '" + txtNome.Text + "', CPF = '" + txtCPF.Text + "'," +
                    "Cargo = '" + txtCargo.Text + "', idade ='" + txtIdade.Text + "' where FuncionarioID = '" + lblId.Text + "'";

                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.CommandType = CommandType.Text;

                sqlConnection.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Atualizado com sucesso");
                        LimpaCampos();
                        Form1_Load(sender, e);
                        btnInserir.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não atualizado" + ex.Message);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
           

            string connection = @"Data Source = DESKTOP-71RT81O\SQLEXPRESS; Initial Catalog = bd_revisao; Integrated Security = True";
            SqlConnection sqlConnection = new SqlConnection(connection);

            string sql = @"delete from funcionario where FuncionarioID = '" + lblId.Text + "'";

            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.CommandType = CommandType.Text;

            sqlConnection.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Dado deletado com sucesso");
                    LimpaCampos();
                    Form1_Load(sender, e);
                    btnInserir.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não deletado" + ex.Message);
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtCPF.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtCargo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtIdade.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            btnInserir.Enabled = false;
            btnDeletar.Enabled = true;
            btnAtualizar.Enabled = true;
        }
        public void LimpaCampos()
        {
            txtNome.Clear();
            txtCPF.Clear();
            txtCargo.Clear();
            txtIdade.Clear();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            btnInserir.Enabled = true;
            lblId.Text = "";
            btnDeletar.Enabled = false;
            btnAtualizar.Enabled = false;
        }

        private void txtCPF_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


