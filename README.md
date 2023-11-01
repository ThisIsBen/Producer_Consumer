# Producer_Consumer

Reference:
this channel seems to be reasonable. the article explains things very well.
[Implement Producer/Consumer patterns using Channel in C#](https://saigontechnology.com/blog/implement-producerconsumer-patterns-using-channel-in-c)

Avoid freezing UI when applying this producer_consumer in a UI program: [here](https://stackoverflow.com/questions/69565851/using-await-task-run-somemethodasync-vs-await-somemethodasync-in-a-ui)





![image](https://github.com/ThisIsBen/Producer_Consumer/assets/8150459/1870f14f-84f7-4773-8751-e8d1f10dd2db)


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


Channel only saves pointers of its content, if its content are of Object type.

If you change the object right after writing to channel, the object you get from channel will be the changed one.


