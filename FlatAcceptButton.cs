namespace Lexicon.Views.Controls
{
    public class FlatAcceptButton : Button
    {
        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }
    }
}