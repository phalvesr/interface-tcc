namespace InterfaceAquisicaoDadosMotorDc.Helpers
{
    internal static class ControlExtensions
    {
        internal static void UpdateTextThreadSafe(this Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(() =>
                {
                    UpdateTextThreadSafe(control, text);
                });
            }
            else
            {
                control.Text = text;
            }
        }

        internal static void UpdateThreadSafe<TParam>(this Control control, Action<Control> updateAction, Action<TParam> invokable)
        {
            if (control.InvokeRequired)
            {
                control?.Invoke(() => control);
            } 
            else
            {
                updateAction?.Invoke(control);
            }
        }

    }
}
