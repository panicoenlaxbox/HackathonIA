---
title: "Dataexchange"
date: 2024-02-23
geekdocCollapseSection: true
---

DataExchange trabaja sobre Sql Server en IaaS, así que se ha optado por una solución basada en trabajos de Sql Server Agent.
La solución está inspirada en https://ola.hallengren.com/sql-server-backup.html.

## ¿Cómo se guardan los backups?

Hay varios jobs, como puede verse en la siguiente imagen, que se encargar de realizar el trabajo necesario usando procedimientos almacenados que se encuentran en la base de datos *ADMINISTRATION* del servidor.

![](/Cloud/Backups/politica-backups/images/jobs-de.png "Jobs")

- **DatabaseBackup - SYSTEM_DATABASES - FULL**

   Backup completo y diario de las bases de datos del sistema (master, model y msdb). 
   Occurs every day at 22:00 UTC

- **DatabaseBackup - USER_DATABASES - DIFF**

   Backups diferenciales de las bases de datos de usuario.
   Occurs every week on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday at 22:00 UTC

- **DatabaseBackup - USER_DATABASES - FULL**

   Backups completos de las bases de datos de usuario.
   Occurs every week on Sunday at 22:00 UTC
   

- **PS ELIMINAR BACKUPS ANTIGUOS EN AZURE STORAGE**

   Elimina backups antiguos, una vez vencido su periodo de retención.
   Occurs every week on Sunday at 22:59. UTC

Si falla algún job se envía a alerts@analytivalways.com


## Dónde se guardan los backups?

Se guardan en Azure Storage, en blobs (https://learn.microsoft.com/en-us/sql/relational-databases/backup-restore/sql-server-backup-to-url?)view=sql-server-ver16#credential

Se encuentran en una cuenta de storage, llamada **srvproduction** en la suscripción **Production**

![](/Cloud/Backups/politica-backups/images/storage-de.png "Storage")

Inicialmente se guardan con el tier *hot*. 
Hay una policy en el storage para pasar los blobs a *cool* (optimización de coste) cuando pasan 24 horas.

Tiers: https://learn.microsoft.com/en-us/azure/storage/blobs/access-tiers-overview 

Como no llegamos a los 90 días de retención, no podemos pasar al siguiente nivel (*cold*)

## TODO

- Modificar el borrado una vez pasado el periodo de retención, ya que trata de borrar backups que ya no existen