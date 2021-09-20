using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private const string connectionString = "server=192.168.79.128; user id=root; password=ph1234; database=descomplicaFinanceiro";
        public Form1()
        {
            InitializeComponent();
        }


        private void Excluir()
        {
            try
            {
                int intRetorno = 0;
                if (txtIdConta.Text == string.Empty)
                {
                    throw new Exception("Informe o id da conta que deseja excluir");
                }

                MySqlConnection mCon = new MySqlConnection(connectionString);
                mCon.Open();

                StringBuilder sql = new StringBuilder();

                sql.Append("delete from contas");
                sql.Append($" where id={txtIdConta.Text}");

                MySqlCommand mCmd = new MySqlCommand(sql.ToString(), mCon);
                intRetorno = mCmd.ExecuteNonQuery();

                if (intRetorno == 1)
                {
                    MessageBox.Show("Excluiu");
                    Listar();
                    LimparTela();
                }
                else
                {
                    MessageBox.Show("Não excluiu");
                }
                sql.Clear();
                mCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Listar()
        {
            try
            {
                MySqlConnection mCon = new MySqlConnection(connectionString);
                mCon.Open();

                StringBuilder sql = new StringBuilder();

                sql.Append("select * from contas");

                MySqlCommand mCmd = new MySqlCommand(sql.ToString(), mCon);
                MySqlDataAdapter mAdp = new MySqlDataAdapter(mCmd);

                DataSet ds = new DataSet();
                mAdp.Fill(ds);

                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.Refresh();

                sql.Clear();
                mCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Editar()
        {
            try
            {
                if (txtIdConta.Text == string.Empty)
                {
                    throw new Exception("Informe o id da conta que deseja editar");
                }

                int intRetorno = 0;
                MySqlConnection mCon = new MySqlConnection(connectionString);
                mCon.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("update contas");
                sql.Append($" set descricao = '{txtDescricao.Text}',");
                sql.Append($" dataConta = '{txtData.Text}',");
                sql.Append($" parcelas = '{txtParcelas.Text}',");
                sql.Append($" valor = '{decimal.Parse(txtValor.Text)}'");
                sql.Append($" where id = '{int.Parse(txtIdConta.Text)}';");

                MySqlCommand mCmd = new MySqlCommand(sql.ToString(), mCon);
                intRetorno = mCmd.ExecuteNonQuery();

                if (intRetorno == 1)
                {
                    MessageBox.Show("Editou");
                    Listar();
                    LimparTela();
                }
                else
                {
                    MessageBox.Show("Não editou");
                }
                sql.Clear();
                mCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Carregar()
        {
            try
            {
                if (txtIdConta.Text == string.Empty)
                {
                    throw new Exception("Informe o id da conta que deseja carregar");
                }

                MySqlConnection mCon = new MySqlConnection(connectionString);
                mCon.Open();

                StringBuilder sql = new StringBuilder();

                sql.Append("select * from contas");
                sql.Append($" where id={decimal.Parse(txtIdConta.Text)}");

                MySqlCommand mCmd = new MySqlCommand(sql.ToString(), mCon);
                MySqlDataAdapter mAdp = new MySqlDataAdapter(mCmd);

                DataSet ds = new DataSet();
                mAdp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow rd = ds.Tables[0].Rows[0];
                    txtData.Text = rd["dataConta"].ToString();
                    txtDescricao.Text = rd["descricao"].ToString();
                    txtIdConta.Text = rd["id"].ToString();
                    txtParcelas.Text = rd["parcelas"].ToString();
                    txtValor.Text = rd["valor"].ToString();
                }
                else
                {
                    MessageBox.Show("Conta não encontrada.");
                }
                sql.Clear();
                mCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Gravar()
        {
            try
            {
                int intRetorno = 0;
                MySqlConnection mCon = new MySqlConnection(connectionString);
                mCon.Open();

                StringBuilder sql = new StringBuilder();

                sql.Append("insert into contas(dataConta, descricao, parcelas, valor)");
                sql.Append($" values('{txtData.Text}', '{txtDescricao.Text}',{int.Parse(txtParcelas.Text)}, {decimal.Parse(txtValor.Text)})");
                MySqlCommand mCmd = new MySqlCommand(sql.ToString(), mCon);
                intRetorno = mCmd.ExecuteNonQuery();

                if (intRetorno == 1)
                {
                    MessageBox.Show("Gravou");
                    Listar();
                    LimparTela();
                }
                else
                {
                    MessageBox.Show("Não gravou");
                }
                sql.Clear();
                mCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LimparTela()
        {
            txtData.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtIdConta.Text = string.Empty;
            txtParcelas.Text = string.Empty;
            txtValor.Text = string.Empty;
        }

        private void btnTestar_Click(object sender, EventArgs e)
        {
            Gravar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            Carregar();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            Listar();
        }
    }
}
