# Piddle.EntityFramework.DisconnectedRepo

Entity Framework is something I'd used in the past (version 1) and it worked for my purposes at the time. In 2013 I came back to using Entity Framework and by mid 2015 I was up to version 6 with Code First migrations, but probably nothing like most people. A colleague of mine asked why I had created my own custom library that wraps Entity Framework and the answer comes down to the issues working with disconnected entities.

I've put this project together to showcase the problems with a "Normal" context (out of the box) and with a "Manual" context (AutoDetectChangesEnabled = false, LazyLoadingEnabled = false, ProxyCreationEnabled = false). There are unit tests that highlight some of what you can and can't do with the two, with the most important being what happens to children when detaching or disposing of a context.

# What you need

- Visual Studio 2013+
- SQL Server Express 2012+ (older might work though)

# How to run

- Create a new DB in SQL Server e.g. Piddle.EntityFramework.DisconnectedRepo.TestingArea
- Load solution in Visual Studio.
- Edit App.config connection string to point at the new DB
- Run the unit tests
