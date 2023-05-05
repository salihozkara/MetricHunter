using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetricHunter.Desktop.Presenters;
using MetricHunter.Desktop.Views;

namespace MetricHunter.Desktop
{
    public partial class ViewFindRepository : Form, IViewFindRepository
    {
        public ViewFindRepository()
        {
            InitializeComponent();
        }

        public void Run()
        {
            ShowDialog();
        }

        public IViewFindRepositoryPresenter Presenter { get; set; }
    }
}
