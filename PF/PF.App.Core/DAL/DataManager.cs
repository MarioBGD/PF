using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PF.App.Core.DAL.Api;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;
using Splat;

namespace PF.App.Core.DAL
{
    public class DataManager : IDataManager
    {
        private LinkedList<DataRequest> DataRequests = new LinkedList<DataRequest>();
        private List<Task> CurrentTasks = new List<Task>();
        public Thread MainThreadHandler;

        private static IDataGetterService OnlineService;
        private static IDataGetterService OfflineService;

        public void Init()
        {
            var sessionManager = Locator.Current.GetService<ISessionManager>();
            OnlineService = new ApiDataGetterService(sessionManager);
            
            MainThreadHandler = new Thread(new ThreadStart(MainThreadProc));
            MainThreadHandler.Start();
        }

        private async void MainThreadProc()
        {
            // if (SessionManager.State == SessionManager.SessionState.Unauthorized)
            // {
            //     var authRes = await SessionManager.Authorize(SessionManager.Username, SessionManager.Password);
            //
            //     if (authRes == SessionManager.SessionState.Unauthorized)
            //     {
            //         
            //         return;
            //     }
            // }

            while (true)
            {
                if (DataRequests.Count <= 0)
                {
                    Thread.Sleep(20);
                    continue;
                }

                DataRequest req = DataRequests.First.Value;
                DataRequests.RemoveFirst();


                IDataGetterService GetterService = OnlineService;

                object result = req.DataType switch
                {
                    DataType.MyUser => await GetterService.GetMyUser(),
                    DataType.Friendships => await GetterService.GetFriendships(),
                    DataType.Groups => await GetterService.GetAllMyGroups(),
                    DataType.Memberships => await GetterService.GetMemberships((Nullable<long>)req.Args),
                    DataType.Users => await GetterService.GetUsers((List<long>)req.Args),
                    DataType.Expenses => await GetterService.GetExpenses((Nullable<long>)req.Args),
                    DataType.Balances => await GetterService.GetBalances((BalanceRequestModel)req.Args),
                    _ => null
                };

                if (result != null)
                    req.Callback(result);
            }
        }


        public void GetData(DataType dataType, IDataManager.DataCallback callback, object args = null, bool highPrio = false)
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
            public IDataManager.DataCallback Callback { get; }
            public object Args { get; }

            public DataRequest(DataType dataType, IDataManager.DataCallback callback, object args = null)
            {
                this.DataType = dataType;
                this.Callback = callback;
                this.Args = args;
            }
        }
    }
}