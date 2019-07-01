using static CoreOHB.Core;

using Windows.ApplicationModel.Background;

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

