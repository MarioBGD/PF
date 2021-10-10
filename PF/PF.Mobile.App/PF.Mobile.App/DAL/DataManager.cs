using PF.Mobile.App.DAL.Api;
using PF.Mobile.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PF.Mobile.App.DAL
{
    public static class DataManager
    {
        public delegate void DataCallback(object data);
        public enum DataType
        {
            MyUser, Friendships, Groups, Memberships, Users, Expenses, Balances
        }

        private static LinkedList<DataRequest> DataRequests = new LinkedList<DataRequest>();
        private static List<Task> CurrentTasks = new List<Task>();
        public static Thread MainThreadHandler;

        private static ISmartClient OnlineClient;
        private static ISmartClient OfflineClient;

        public static void Init()
        {
            MainThreadHandler = new Thread(new ThreadStart(MainThreadProc));
            OnlineClient = new ApiClient();

            MainThreadHandler.Start();
        }

        private static async void MainThreadProc()
        {
            if (SessionManager.State == SessionManager.SessionState.Unauthorized)
            {
                var authRes = await SessionManager.Authorize(SessionManager.Username, SessionManager.Password);

                if (authRes == SessionManager.SessionState.Unauthorized)
                {
                    
                    return;
                }
            }

            while (true)
            {
                if (DataRequests.Count <= 0)
                {
                    Thread.Sleep(10);
                    continue;
                }

                DataRequest req = DataRequests.First.Value;
                DataRequests.RemoveFirst();


                ISmartClient Client = OnlineClient;

                object result = req.DataType switch
                {
                    DataType.MyUser => await Client.GetMyUser(),
                    DataType.Friendships => await Client.GetFriendships(),
                    DataType.Groups => await Client.GetGroups((List<long>)req.Args),
                    DataType.Memberships => await Client.GetMemberships((Nullable<long>)req.Args),
                    DataType.Users => await Client.GetUsers((List<long>)req.Args),
                    DataType.Expenses => await Client.GetExpenses((Nullable<long>)req.Args),
                    DataType.Balances => await Client.GetBalances((BalanceRequestModel)req.Args),
                    _ => null
                };

                if (result != null)
                    req.Callback(result);
            }
        }


        public static void GetData(DataType dataType, DataCallback callback, object args = null, bool highPrio = false)
        {
             DataRequest dataRequest = new  DataRequest(dataType, callback, args);

            if (highPrio)
                DataRequests.AddFirst(dataRequest);
            else
                DataRequests.AddLast(dataRequest);
        }

        private class DataRequest
        {
            public DataType DataType { get; }
            public DataCallback Callback { get; }
            public object Args { get; }

            public DataRequest(DataType dataType, DataCallback callback, object args = null)
            {
                this.DataType = dataType;
                this.Callback = callback;
                this.Args = args;
            }
        }
    }
}
