# GetUsersFileSystemTree
## Task

Программа при запуске должна построить Дерево файловой системы для текущего пользователя. Файлы, которые попадают в результирующее дерево
должны быть созданы позднее 15 дней назад.

Результат записать в файл, который затем должен быть сжат и размещен на рабочем столе пользователя.
Файлы, которые закрыты правами доступа программа должна пропускать (т.е. не должно быть exception-ов).

Папки, которые не содержат внутри своей иерархии подходящего файла выводиться не должны.
