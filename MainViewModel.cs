using System;
using System.Windows;
using practika_2.Core;

namespace practika_2.Wpf.ViewModels
{
    /// <summary>
    /// ViewModel для карточки сайта в списке хостинг-платформы.
    /// Отображает информацию о домене, тарифе, владельце и использовании диска.
    /// </summary>
    public class SiteCardViewModel : ViewModelBase
    {
        #region Свойства отображения

        private string _domain;
        /// <summary>Доменное имя сайта.</summary>
        public string Domain
        {
            get => _domain;
            set { _domain = value; OnPropertyChanged(); }
        }

        private string _planName;
        /// <summary>Название тарифного плана.</summary>
        public string PlanName
        {
            get => _planName;
            set { _planName = value; OnPropertyChanged(); }
        }

        private string _ownerName;
        /// <summary>ФИО владельца сайта.</summary>
        public string OwnerName
        {
            get => _ownerName;
            set { _ownerName = value; OnPropertyChanged(); }
        }

        private string _statusText;
        /// <summary>Текстовое отображение статуса сайта.</summary>
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(); }
        }

        private string _statusColor;
        /// <summary>Цвет индикатора статуса в формате HEX.</summary>
        public string StatusColor
        {
            get => _statusColor;
            set { _statusColor = value; OnPropertyChanged(); }
        }

        private int _diskUsagePercent;
        /// <summary>Процент использования дискового пространства (0-100).</summary>
        public int DiskUsagePercent
        {
            get => _diskUsagePercent;
            set { _diskUsagePercent = value; OnPropertyChanged(); }
        }

        private string _diskUsageText;
        /// <summary>Текст использования диска, например "2.4 ГБ / 10 ГБ".</summary>
        public string DiskUsageText
        {
            get => _diskUsageText;
            set { _diskUsageText = value; OnPropertyChanged(); }
        }

        private string _diskBarColor;
        /// <summary>Цвет прогресс-бара использования диска в формате HEX.</summary>
        public string DiskBarColor
        {
            get => _diskBarColor;
            set { _diskBarColor = value; OnPropertyChanged(); }
        }

        private string _extraDetails;
        /// <summary>Дополнительная информация о сайте (показывается при наведении).</summary>
        public string ExtraDetails
        {
            get => _extraDetails;
            set { _extraDetails = value; OnPropertyChanged(); }
        }

        #endregion

        #region Команды

        /// <summary>Команда редактирования настроек сайта.</summary>
        public RelayCommand<SiteCardViewModel> EditCommand { get; }

        /// <summary>Команда управления сайтом (переход в панель управления).</summary>
        public RelayCommand<SiteCardViewModel> ManageCommand { get; }

        /// <summary>Команда оплаты хостинга за сайт.</summary>
        public RelayCommand<SiteCardViewModel> PayCommand { get; }

        #endregion

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SiteCardViewModel"/> 
        /// и регистрирует команды.
        /// </summary>
        public SiteCardViewModel()
        {
            EditCommand = new RelayCommand<SiteCardViewModel>(OnEdit, _ => true);
            ManageCommand = new RelayCommand<SiteCardViewModel>(OnManage, _ => true);
            PayCommand = new RelayCommand<SiteCardViewModel>(OnPay, _ => true);
        }

        /// <summary>
        /// Обработчик команды редактирования сайта.
        /// </summary>
        private void OnEdit(SiteCardViewModel site)
        {
            MessageBox.Show(
                $"Редактирование сайта:\nДомен: {site.Domain}\nТариф: {site.PlanName}",
                "✏️ Редактирование",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Обработчик команды управления сайтом.
        /// </summary>
        private void OnManage(SiteCardViewModel site)
        {
            MessageBox.Show(
                $"Панель управления:\n{site.Domain}\nСтатус: {site.StatusText}",
                "⚙️ Управление",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Обработчик команды оплаты хостинга.
        /// </summary>
        private void OnPay(SiteCardViewModel site)
        {
            MessageBox.Show(
                $"Оплата хостинга:\nСайт: {site.Domain}\nТариф: {site.PlanName}\n{site.DiskUsageText}",
                "💳 Оплата",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}