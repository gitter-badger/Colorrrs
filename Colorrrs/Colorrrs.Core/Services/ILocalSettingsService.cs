using System.Collections.Generic;

namespace Colorrrs.Core.Services
{
    public interface ILocalSettingsService
    {
        void SaveComposite(string key, IDictionary<string, object> values);

        bool CanRetrieveComposite(string key);
        IDictionary<string, object> RetrieveComposite(string key);
    }
}
