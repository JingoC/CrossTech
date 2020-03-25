# Тестовое задание в CrossTech

Исходный файл задания (с помеченными выполненными пунктами): https://github.com/JingoC/CrossTech/blob/master/Files/%D0%97%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5%20ASP.NET.docx

Пункт про User не был выполнен по той причине, что он абсолютно идентичен Employee в своей реализации.

# Структура проекта

Чтобы не перегружать решение, все репозитории лежат в одном solution, но в реальности они должны быть разбиты на 3 solution:

+ Общая библиотека
    + CrossTech.Core
+ Микросервис с логикой
    + CrossTech.ClientApi
    + CrossTechTask.DAL
    + CrossTech.WebApi
+ Веб-приложение клиента
    + CrossTechTask.WebApp

### Общая библиотека

_CrossTech.Core_ - Реализует набор сервисных классов, регулярно применяющихся во множестве микросервисов. Выпускается в виде Nuget пакета и подключается к требуемым репозиториям.

Данная библиотека содержит следующие классы:

_BaseRemoteCallService_ - базовый класс для реализации Клиентского API.

_BaseRepository_ (**реализация позаимствована!!**) - базовый класс для реализации шаблона Repository к DB.

### Микросервис с логикой

_CrossTechTask.DAL_ - Слой работы с базой данных. 

_CrossTech.WebApi_ - Микросервис реализующий API для проведения операций над 'Сотрудниками'.

_CrossTech.ClientApi_ - Клиентское API, выпускается в виде Nuget пакета, и подключается ко всем сервисам интегрирующимся с CrossTech.WebApi.

### Веб-приложение клиента

_CrossTechTask.WebApp_ - микросервис Веб-приложения, используемого на клиентской стороне.

# Запуск

Реализовано в Microsoft Visual Studio 2019.

1. Выполнить скрипт для генерации БД в SSMS: [https://github.com/JingoC/CrossTech/blob/master/Files/crosstech_create_db_utf16.sql]
2. Установить npm для проекта **CrossTechTask.WebApp**
3. Для минификации js используется Bundler & Minifier, требуется установить его как Extension в VS. Выполнить Task на генерацию min.js\min.css (**требуется только если будут вноситься изменения**)
4. Запустить два проекта одновременно: **CrossTechTask.WebApp** и **CrossTech.WebApi**.
5. В окне логина пароля ввести данные [login]-[password]:

С ролью Admin:  [admin]-[111]

С ролью User: [user1]-[222]

