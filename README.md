# Producer_Consumer

Reference:
this channel seems to be reasonable. the article explains things very well.
[Implement Producer/Consumer patterns using Channel in C#](https://saigontechnology.com/blog/implement-producerconsumer-patterns-using-channel-in-c)


![image](https://github.com/ThisIsBen/Producer_Consumer/assets/8150459/1870f14f-84f7-4773-8751-e8d1f10dd2db)



Avoid freezing UI when applying this producer_consumer in a UI program: [here](https://stackoverflow.com/questions/69565851/using-await-task-run-somemethodasync-vs-await-somemethodasync-in-a-ui)
Why UI program run the rest of the program after await with the thread that called await?
[here](https://www.pluralsight.com/guides/using-task-run-async-await)
![image](https://github.com/ThisIsBen/Producer_Consumer/assets/8150459/3d6f953e-00c5-41c2-8a4d-009709ae5329)

Launching an operation on a separate thread via Task.Run is mainly useful for CPU-bound operations, not I/O-bound operations.

When using simply run an async function to activate a consumer



![image](https://github.com/ThisIsBen/Producer_Consumer/assets/8150459/7a86ef16-ef64-4e1b-a3a4-b9094e9c605f)



After using Task.run to activate a consumer




![image](https://github.com/ThisIsBen/Producer_Consumer/assets/8150459/a216165c-ecd5-4d32-9247-911db2c5f750)





-Use unbounded channel 
An UnboundedChannel can be useful in scenarios where the number of messages being produced is not known in advance, or where the producer and consumer are processing at different speeds. 

However, it is important to note that an unbounded channel can potentially consume a large amount of memory if the rate of production exceeds the rate of consumption, so it should be used with care.




-Set SingleWriter to true.
Because if this property is set to true, the channel may be able to optimize some certain operations based on the assumption that there is only one writer.


-Set SingleReader to true.(If we only use 1 consumer)
Because if this property is set to true, the channel may be able to optimize some certain operations based on the assumption that there is only one writer.


If bounded channel is used, and the buffer is full,
Channel provides the following measures.
BoundedChannelFullMode.DropWrite(Drop every write when the buffer is full)
If buffer size is 10,
0...9  
10..99 are dropped.

BoundedChannelFullMode.DropOldest 
90...99

BoundedChannelFullMode.DropNewest
0...8,99

Channelから内容取得：
'''
while (await _reader.WaitToReadAsync()) //ここでChannel内に内容が追加されるのを待っている
                {
                    string oriPath= "";
                    
                    //Consume the data in the queue
                    if (_reader.TryRead(out oriPath))
                    {
                               //Channelから取り出した内容を処理する
		   }
		
		}
 //Channelがcompleteとマークされたら、WaitToReadAsyncはfalseを返し、ここにくる。
 Console.WriteLine($"CONSUMER ({_identifier}): 終了");
'''

 WaitToReadAsync　will complete with a true result when data is available to read or with a false result when no further data will ever be available to be read due to the channel completing successfully.
![image](https://github.com/ThisIsBen/Producer_Consumer/assets/8150459/eb992cb0-3492-4115-b21d-e42f5cd3dc66)


Channel only saves pointers of its content, if its content are of Object type.

If you change the object right after writing to channel, the object you get from channel will be the changed one.




