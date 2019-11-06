
using Windows.ApplicationModel.Background;
using static CoreOHB.Core;
using static CoreOHB.Helpers.Files;

namespace RuntimeComponentOHB
{
    public sealed class YMLShopUpdate : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            try
            {
                await UpdateOneHomeBeautyAsync();
            }
            finally
            {
                await LogAsync($"{this.ToString()} complete!");
                _deferral.Complete();
            }
        }
        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            // Indicate that the background task is canceled.
            //_cancelRequested = true;

            //Debug.WriteLine("Background " + sender.Task.Name + " Cancel Requested...");
        }
    }
}

