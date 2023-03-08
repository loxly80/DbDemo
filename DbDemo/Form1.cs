using System.Data.SqlClient;

namespace DbDemo
{
  public partial class Form1 : Form
  {
    SqlRepo repo;

    public Form1()
    {
      InitializeComponent();
      repo = new SqlRepo();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      LoadData();
    }

    private void LoadData()
    {
      listView1.Items.Clear();
      foreach (var item in repo.LoadData())
      {
        listView1.Items.Add(item.ToListViewItem());
      }
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
      if(listView1.SelectedIndices.Count > 0)
      {
        var id = listView1.Items[listView1.SelectedIndices[0]].SubItems[0].Text;
        repo.DeleteItem(id);
        LoadData();
      }
      else
      {
        MessageBox.Show("Vyber položku");
      }
    }
  }
}