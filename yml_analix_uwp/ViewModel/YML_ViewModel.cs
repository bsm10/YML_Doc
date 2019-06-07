using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YML_Doc.ViewModel
{
    public class ViewModelCatalog
    {
        public ObservableCollection<YML_Catalog> YMLCatalog { get; } = new ObservableCollection<YML_Catalog>();
    }
}
