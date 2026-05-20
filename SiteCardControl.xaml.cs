using practika_2.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace practika_2.Wpf.Controls
{
    /// <summary>
    /// Карточка отображения сайта в списке хостинг-платформы.
    /// Поддерживает привязку данных и команды для взаимодействия.
    /// </summary>
    public partial class SiteCardControl : UserControl
    {
        #region Dependency Properties

        /// <summary>
        /// Команда редактирования сайта.
        /// </summary>
        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register(nameof(EditCommand), typeof(ICommand), typeof(SiteCardControl));

        /// <summary>
        /// Команда управления сайтом.
        /// </summary>
        public static readonly DependencyProperty ManageCommandProperty =
            DependencyProperty.Register(nameof(ManageCommand), typeof(ICommand), typeof(SiteCardControl));

        /// <summary>
        /// Команда оплаты хостинга.
        /// </summary>
        public static readonly DependencyProperty PayCommandProperty =
            DependencyProperty.Register(nameof(PayCommand), typeof(ICommand), typeof(SiteCardControl));

        /// <summary>
        /// Показывать дополнительную информацию при наведении.
        /// </summary>
        public static readonly DependencyProperty ShowExtraInfoProperty =
            DependencyProperty.Register(nameof(ShowExtraInfo), typeof(bool), typeof(SiteCardControl),
                new PropertyMetadata(false, OnShowExtraInfoChanged));

        #endregion

        #region Public Properties

        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        public ICommand ManageCommand
        {
            get => (ICommand)GetValue(ManageCommandProperty);
            set => SetValue(ManageCommandProperty, value);
        }

        public ICommand PayCommand
        {
            get => (ICommand)GetValue(PayCommandProperty);
            set => SetValue(PayCommandProperty, value);
        }

        public bool ShowExtraInfo
        {
            get => (bool)GetValue(ShowExtraInfoProperty);
            set => SetValue(ShowExtraInfoProperty, value);
        }

        #endregion

        public SiteCardControl()
        {
            InitializeComponent();
            DataContextChanged += SiteCardControl_DataContextChanged;
        }

        private void SiteCardControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Автоматическая привязка команд из ViewModel, если они не заданы явно
            if (DataContext is SiteCardViewModel vm)
            {
                if (EditCommand == null && vm.EditCommand != null)
                    EditCommand = vm.EditCommand;
                if (ManageCommand == null && vm.ManageCommand != null)
                    ManageCommand = vm.ManageCommand;
                if (PayCommand == null && vm.PayCommand != null)
                    PayCommand = vm.PayCommand;
            }
        }

        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (ShowExtraInfo)
                ExtraInfo.Visibility = Visibility.Visible;
        }

        private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ExtraInfo.Visibility = Visibility.Collapsed;
        }

        private static void OnShowExtraInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SiteCardControl control && e.NewValue is bool show)
            {
                control.ExtraInfo.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}