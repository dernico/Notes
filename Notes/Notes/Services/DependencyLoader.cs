using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes.Services
{
    public class DependencyLoader
    {

        public static T New<T>() where T : class
        {
            return DependencyService.Get<T>(DependencyFetchTarget.NewInstance);
        }

        public static T Singleton<T>() where T : class
        {
            return DependencyService.Get<T>(DependencyFetchTarget.GlobalInstance);
        }
    }
}
