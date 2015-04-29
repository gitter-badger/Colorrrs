using System.Collections.Generic;
using Windows.Storage;
using Colorrrs.Core.Services;

namespace Colorrrs.Services
{
    public class LocalSettingsService : ILocalSettingsService
    {
        public void SaveComposite(string key, IDictionary<string, object> values)
        {
            var composite = new ApplicationDataCompositeValue();
            
            foreach (var kvp in values)
                composite[kvp.Key] = kvp.Value;
            
            ApplicationData.Current.LocalSettings.Values[key] = composite;
        }

        public bool CanRetrieveComposite(string key)
        {
            return (ApplicationData.Current.LocalSettings.Values[key] as ApplicationDataCompositeValue != null);
        }
        public IDictionary<string, object> RetrieveComposite(string key)
        {
            return ApplicationData.Current.LocalSettings.Values[key] as ApplicationDataCompositeValue;
        }
    }
}
