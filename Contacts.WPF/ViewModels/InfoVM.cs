using Contacts.WPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.WPF.ViewModels
{
    /// <summary>
    /// Вью модель окна информации о приложении
    /// </summary>
    public class InfoVM : BaseVM
    {
        #region Properties
        /// <summary>
        /// Загловок
        /// </summary>
        public override string Title => "Информация о приложении";
        #endregion
    }
}
