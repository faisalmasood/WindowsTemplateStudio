﻿//{[{
using Param_ItemNamespace.ViewModels;
//}]}
namespace Param_ItemNamespace.Activation
{
    internal class SchemeActivationHandler : ActivationHandler<ProtocolActivatedEventArgs>
    {
//{[{
        private NavigationServiceEx NavigationService
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }

        // By default, this handler expects URIs of the format 'wtsapp:sample?secret={value}'
        protected override async Task HandleInternalAsync(ProtocolActivatedEventArgs args)
        {
            if (args.Uri.AbsolutePath.ToLowerInvariant().Equals("sample"))
            {
                var secret = "<<I-HAVE-NO-SECRETS>>";

                try
                {
                    if (args.Uri.Query != null)
                    {
                        // The following will extract the secret value and pass it to the page. Alternatively, you could pass all or some of the Uri.
                        var decoder = new Windows.Foundation.WwwFormUrlDecoder(args.Uri.Query);

                        secret = decoder.GetFirstValueByName("secret");
                    }
                }
                catch (Exception)
                {
                    // NullReferenceException if the URI doesn't contain a query
                    // ArgumentException if the query doesn't contain a param called 'secret'
                }

                // It's also possible to have logic here to navigate to different pages. e.g. if you have logic based on the URI used to launch
                NavigationService.Navigate(typeof(ViewModels.wts.ItemNameExampleViewModel).FullName, secret);
            }
            else if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                // If the app isn't running and not navigating to a specific page based on the URI, navigate to the home page
                NavigationService.Navigate(typeof(ViewModels.MainViewModel).FullName);
            }

            await Task.CompletedTask;
        }

//}]}
    }
}
