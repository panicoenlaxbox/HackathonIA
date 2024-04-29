---
title: "VM-PRODUCTION (DP / MSS)"
date: 2024-02-23
geekdocCollapseSection: true
---

VM-PRODUCTION tiene (además de otras cosas) Sql Server en IaaS, así que se ha optado por una solución basada en trabajos de Sql Server Agent.
La solución está inspirada en https://ola.hallengren.com/sql-server-backup.html.

## ¿Cómo se guardan los backups?

Hay varios jobs, como puede verse en la siguiente imagen, que se encargar de realizar el trabajo necesario usando procedimientos almacenados que se encuentran en la base de datos *ADMINISTRATION* de cada servidor.

![](/Cloud/Backups/politica-backups/images/jobs-dp.png "Jobs")

- **DatabaseBackup - SYSTEM_DATABASES - FULL**

   Backup completo y diario de las bases de datos del sistema (master, model y msdb). 
   Occurs every day at 22:00 UTC

- **DatabaseBackup - USER_DATABASES - DIFF**

   Backups diferenciales de las bases de datos de usuario.
   Occurs every week on Monday, Tuesday, Wednesday, Thursday, Friday, Saturday at 23:30

- **DatabaseBackup - USER_DATABASES - FULL**

   Backups completos de las bases de datos de usuario.
   Occurs every week on Sunday at 23:00:00

Si falla algún job se envía a alerts@analytivalways.com

## Dónde se guardan los backups?

Se guardan en Azure Storage, en blobs (https://learn.microsoft.com/en-us/sql/relational-databases/backup-restore/sql-server-backup-to-url?)view=sql-server-ver16#credential

Se encuentran en una cuenta de storage, llamada **srvproduction** en la suscripción **Production**

![](/Cloud/Backups/politica-backups/images/storage-dp.png "Storage")

Inicialmente se guardan con el tier *hot*. 
Hay una policy en el storage para pasar los blobs a *cool* (optimización de coste) cuando pasan 24 horas.

Tiers: https://learn.microsoft.com/en-us/azure/storage/blobs/access-tiers-overview 

Como no llegamos a los 90 días de retención, no podemos pasar al siguiente nivel (*cold*)

## TODO

- Averiguar como se borran los backups antiguos, no hay job
- Ver como se hacen los backups anuales y mensuales, porque no hay job para ello
