# Piddle.EntityFramework.DisconnectedRepo

Entity Framework is something I'd used in the past (it was version 4) and it worked for my purposes at the time. Two years ago I came back to using Entity Framework and by mid 2015 I was up to version 6 with Code First migrations, but probably nothing like most people. A colleague of mine asked why I had created my own custom library that wraps Entity Framework and the answer comes down to the issues working with disconnected entities.

I've put this project together to showcase the problems with a "Normal" context (out of the box) and with a "Manual" context (AutoDetectChangesEnabled = false, LazyLoadingEnabled = false, ProxyCreationEnabled = false). There are unit tests that highlight some of what you can and can't do with the two, with the most important being what happens to children when detaching or disposing of a context.
