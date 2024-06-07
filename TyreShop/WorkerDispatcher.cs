using System.Collections.Concurrent;
using TireShop.Configurations;

namespace TireShop;

public class WorkerDispatcher
{
    private readonly List<MechanicWorker> _pool;
    private readonly ConcurrentQueue<Customer> _customerQueue;
    private readonly TaskCompletionSource _completionSource = new();
    private readonly List<Task> _workerTasks = new();

    public WorkerDispatcher(int workerCount, IStorage storage, MechanicConfiguration mechanicConfiguration)
    {
        _pool = InitializeWorkers(workerCount, storage, mechanicConfiguration);
        _customerQueue = new ConcurrentQueue<Customer>();
    }

    // Start workers and their tasks for checking state.
    public void Start() => _pool.ForEach(m => _workerTasks.Add(m.DoWork(_completionSource.Task)));

    public void Stop()
    {
        // Signal emergency stop of work and kick all customers.
        _completionSource.SetCanceled();
        _customerQueue.Clear();
    }

    // Signal workers to start completing work.
    public void StartCompletingWork() => _completionSource.TrySetResult();

    public Task IsWorkCompleted() => Task.WhenAll(_workerTasks.ToArray());

    public void AddCustomerToWaitQueue(Customer customer) => _customerQueue.Enqueue(customer);

    private List<MechanicWorker> InitializeWorkers(int workerCount, IStorage storage, MechanicConfiguration mechanicConfiguration)
    {
        List<MechanicWorker> workers = new();
        for (int i = 0; i < workerCount; i++)
        {
            workers.Add(new MechanicWorker(storage,
                                           mechanicConfiguration,
                                           GetNextCustomer,
                                           () => !_customerQueue.IsEmpty));
        }

        return workers;
    }

    private Customer GetNextCustomer() => _customerQueue.TryDequeue(out var customer) ? customer : null;
}