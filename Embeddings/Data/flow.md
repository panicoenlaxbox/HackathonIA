---
title: "Flow manager"
date: 2024-02-26
geekdocCollapseSection: true
---

Es bases de datos Sql Azure. 

## ¿Cómo se guardan los backups?

Se usa un runbook para hacer los backups. La limpieza es hecha por el runbook de las bases de datos de tecnología

**Runbooks**(en autoaccount-analyticalways): 

- dr-backup-azure-sql-flowmanager: Hace el backup, para ello dispone de  una programación que lo ejecuta diariamente a las 15 (Central European Time)
- Backup-Remove-TechnologyDBs-Old-Days: Elimina una vez pasado el periodo de retención

Bases de datos:

- sqldb-salesforce-exchange
- RoivolutionDistCache
- BackgroundTasksScheduler
- Evalutrix
- IdentityRoivolutionDbPro
- soyuz
- sqldb-datasaviour

## Dónde se guardan los backups?

Se guardan en Azure Storage, en la storage account **staasqltechbackupweu ** de la suscripciómn **mirror**, por un tiempo de 1 día.

![](/Cloud/Backups/politica-backups/images/storage-tech.png "Storage")

Inicialmente se guardan con el tier *hot*, y como solo viven 1 día así se quedan.


