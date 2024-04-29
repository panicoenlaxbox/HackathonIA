---
title: "OMP"
date: 2024-02-22
geekdocCollapseSection: true
---

OMP trabaja sobre Sql Server en IaaS, así que se ha optado por una solución basada en trabajos de Sql Server Agent.
La solución está inspirada en https://ola.hallengren.com/sql-server-backup.html.

## ¿Cómo se guardan los backups?

Hay varios jobs, como puede verse en la siguiente imagen, que se encargar de realizar el trabajo necesario usando procedimientos almacenados que se encuentran en la base de datos *ADMINISTRATION* de cada servidor.

![](/Cloud/Backups/politica-backups/images/jobs.png "Jobs")

- **DatabaseBackup - SYSTEM_DATABASES - FULL**

   Backup completo y diario de las bases de datos del sistema (master, model y msdb). 
   Occurs every day at 23:00 UTC

- **DatabaseBackup - USER_DATABASES - DIFF**

   Backups diferenciales de las bases de datos de usuario
   SRV 4: Occurs every week on Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday at 02:00 UTC
   SRV 5: Occurs every day at 2:00:00. Schedule will be used starting on 16/03/2023.

- **DatabaseBackup - USER_DATABASES - FULL**

   Backups completos de las bases de datos de usuario
   
   SRV 4: Occurs every week on Monday at 2:00 UTC
   SRV 5: Occurs every week on Sunday at 23:00:00

- **PS ELIMINAR BACKUPS ANTIGUOS EN AZURE STORAGE**

   Elimina backups antiguos, una vez vencido su periodo de retención.
   Se ejecuta los lunes a las 04:00 UTC


Si falla algún job se envía a alerts@analytivalways.com

## Dónde se guardan los backups?

Se guardan en Azure Storage, en blobs (https://learn.microsoft.com/en-us/sql/relational-databases/backup-restore/sql-server-backup-to-url?)view=sql-server-ver16#credential

Se encuentran en una cuenta de storage, llamada **srvproduction** en la suscripción **Production**

![](/Cloud/Backups/politica-backups/images/storage.png "Storage")

Inicialmente se guardan con el tier *hot*. 
Hay una policy en el storage para pasar los blobs a *cool* (optimización de coste) cuando pasan 24 horas.

Tiers: https://learn.microsoft.com/en-us/azure/storage/blobs/access-tiers-overview 

Como no llegamos a los 90 días de retención, no podemos pasar al siguiente nivel (*cold*)

## TODO

- Alinear horas
- Modificar el borrado una vez pasado el periodo de retención, ya que trata de borrar backups que ya no existen