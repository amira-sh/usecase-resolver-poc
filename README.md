# usecase-resolver-poc

Provides a minimal implementation for a mediator facilitating the execution of usecases. This is to connect requests from the Controllers to the expected usecase automatically so that calling the usecase directly can be avoided. The navigation is based on the input to the usecase meaning usecases always have to have an input of type `IUsecaseRequest`. At the moment it supports usecases having an output (of type `IUsecaseResponse`). In further steps it can also supprt usecases not providing an output.

Benefits:
- Usecases across the application will follow the same design and signature: having no base design makes developers to be so flexible with naming; feeding Usecases with multiple inputs; having multiple public methods in the same usecase contradicting the existence of a usecase.
- Considering Controllers can have multiple Actions result in injecting multiple Usecases in the constrcutor, now only `IUCMediator` needs to be injected to the Controllers result in less polluted constructors.
- Decouples the usecases one more level from outside of the Application layer.

Usage: 
- Add the mediator by providing the assemblies hosting the Usecases: see [this](https://github.com/amira-sh/usecase-resolver-poc/blob/6e2ed228fbb7a9677c00c10fd5823c7cd428a872/Application/ServiceCollectionExtension.cs#L11)
- Make Usecases implement the generic interface `IUsecaseAsync`: see [this](https://github.com/amira-sh/usecase-resolver-poc/blob/6e2ed228fbb7a9677c00c10fd5823c7cd428a872/Application/Usecases/UsecaseA.cs#L6)
- Execute the expected Usecase by providing the request model and expected response: see [this](https://github.com/amira-sh/usecase-resolver-poc/blob/6e2ed228fbb7a9677c00c10fd5823c7cd428a872/usecase-resolver-poc/Controllers/MainController.cs#L15)