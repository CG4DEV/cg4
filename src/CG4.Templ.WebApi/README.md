## Использовать шаблон для Web API проекта

### Скачивание шаблона
Для скачивания шаблана нужно в терминале исполнить следующую команду:<br/>
SDK .NET 7 ```dotnet new install CG4.Template.WebAPI```<br/>
SDK .NET 6 ```dotnet new --install CG4.Template.WebAPI```

После установки шаблона шаблон с именем <b>CG4 API</b> появится в окне создания нового проекта

### Обновление шаблона
Для обновления шаблона нужно в терминале исполнить следующую команду:<br/>
SDK .NET 7 ```dotnet new install CG4.Template.WebAPI --force```<br/>
SDK .NET 6 ```dotnet new --install CG4.Template.WebAPI --force```

### Удаление шаблона
Для удаления шаблона нужно в терминале исполнить следующую команду:<br/>
SDK .NET 7 ```dotnet new uninstall CG4.Template.WebAPI```<br/>
SDK .NET 6 ```dotnet new --uninstall CG4.Template.WebAPI```

### Known Issues
#### Создание шаблона в Rider
При создании шаблона в Rider, в структуре проекта нет папок решения.<br/>
Это связанно с тем что Rider создаёт свой файл .sln, игнорируя таковой из шаблона.<br/>
Что бы решить эту проблему можно:
- Создать проект в Visual Studio, и импортировать затем в Rider.
- Создать проект с помощью dotnet - ```dotnet new cg4api -o {имя проекта}```, и затем импортировать его в Rider.
- Создать проект в Rider, но затем заменить .sln созданный Rider'ом, на .sln из папки шаблона, но необходимо затем исправить пути к проектам.
