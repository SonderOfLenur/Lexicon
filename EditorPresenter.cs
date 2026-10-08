using Lexicon.Views;
using System.Runtime.CompilerServices;

namespace Lexicon.Presenters
{
    internal class EditorPresenter
    {
        private bool IsDarkTheme = false;
        public bool isTextChanged { get; private set; }
        public string? currentFilePath { get; private set; }
        private readonly EditorForm _editorForm;

        public EditorPresenter(EditorForm editorForm)
        {
            _editorForm = editorForm;
        }

        public void SaveFile(string path)
        {
            string ext = Path.GetExtension(path).ToLower();

            if (ext == ".txt")
            {
                _editorForm.SaveTxtContent(path);
            }
            else
            {
                _editorForm.SaveRTFContent(path);
            }

            currentFilePath = path;
            isTextChanged = false;
        }

        public void OpenFile(string path)
        {
            string ext = Path.GetExtension(path).ToLower();

            try
            {
                if (ext == ".txt")
                {
                    _editorForm.OpenTxtContent(path);
                }
                else
                {
                    _editorForm.OpenExtensionContent(path);
                }

                currentFilePath = path;
                isTextChanged = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии файла:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MarkAsChanged()
        {
            isTextChanged = true;
        }

        public void AutoSave()
        {
            if (!string.IsNullOrEmpty(currentFilePath) && isTextChanged)
            {
                SaveFile(currentFilePath);
            }
        }

        public int CountWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;

            int wordCount = 0;
            bool inWord = false;

            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]))
                {
                    inWord = false;
                }
                else if (!inWord)
                {
                    wordCount++;
                    inWord = true;
                }
            }
            return wordCount;
        }
    }
}