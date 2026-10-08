using System;
using Lexicon.Views;

namespace Lexicon.Presenters
{
    /// <summary>
    /// Презентер для управления настройками приложения (загрузка, отображение и сохранение).
    /// </summary>
    internal class SettingsPresenter
    {
        private readonly SettingsForm _settingsForm;
        private AppSettings _currentSettings;

        public SettingsPresenter(SettingsForm settingsForm)
        {
            _settingsForm = settingsForm ?? throw new ArgumentNullException(nameof(settingsForm));

            // 1. При инициализации загружаем сохраненную конфигурацию из XML
            _currentSettings = SettingsManager.Load();

            // 2. Передаем значения из объекта настроек в форму
            ApplySettingsToView();
        }

        /// <summary>
        /// Заполняет элементы интерфейса формы значениями из модели настроек
        /// </summary>
        public void ApplySettingsToView()
        {
            _settingsForm.IsDarkTheme = _currentSettings.IsDarkTheme;
            _settingsForm.IsAutoSaveEnabled = _currentSettings.IsAutoSaveEnabled;
        }

        /// <summary>
        /// Считывает состояния с формы и сохраняет их в config.xml
        /// </summary>
        public void SaveSettings()
        {
            // Считываем актуальные значения из формы в модель
            _currentSettings.IsDarkTheme = _settingsForm.IsDarkTheme;
            _currentSettings.IsAutoSaveEnabled = _settingsForm.IsAutoSaveEnabled;

            // Сохраняем модель через SettingsManager
            SettingsManager.Save(_currentSettings);
        }
    }
}