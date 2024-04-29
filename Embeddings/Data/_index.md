---
title: "Política de backups"
date: 2023-10-03
geekdocCollapseSection: true
---

## Backups "funcionales"

Permiten recuperar información para realizar análisis de negocio a posterior. Por ejemplo, analizar un informe que se dió a un cliente en el pasado.
NO están destinados a recuperación de desastres.

| Aplicación | Periodicidad de realización del backup | Tiempo retención | Descripción | Ámbito | Detalles | 
| --- | --- | --- | --- | --- | --- |
| ROI reporting db's | Diaria | 2 meses | Se hace después de cada recarga. | Clientes |[Detalles](/Cloud/Backups/politica-backups/inseason-reporting )|
| ROI parameter db's | Diaria | Indefinido | Se hace a partir de las 05:00 AM (Central European Time)| Clientes |[Detalles](/Cloud/Backups/politica-backups/inseason-parameters )|
| ISE - Informe Replenishment | Diaria | Indefinido | Copia el directorio "gold/historical/\*". Se hace después de cada recarga. | Clientes | [Detalles](/Cloud/Backups/politica-backups/storages ) |
| ISE - Histórico de proyecciones | Semanal | 2 años cerrados y el año en curso | Copia la proyección de la semana anterior de las carpetas "inseason/projections_\*" e "inseason/prepared_data_for_anual_projection_\*" | Clientes | [Detalles](/Cloud/Backups/politica-backups/storages ) |
| OMP DB's | Diaria | 2 meses | Backups de bases de datos de usuario y sistema | Clientes | [Detalles](/Cloud/Backups/politica-backups/omp) |
| DataExchange DB's | Diaria | 2 meses | Backups de bases de datos de usuario y sistema | Clientes | [Detalles](/Cloud/Backups/politica-backups/dataexchange)  |
| Sales Reporting | Diaria | 2 meses | Se hace después de cada recarga (múltiples recargas al día) | Clientes |[Detalles](/Cloud/Backups/politica-backups/sales-reporting )|
| VM-PRODUCTION (bases de datos) | Diaria | 2 meses | Backups de bases de datos de usuario y sistema | Clientes |[Detalles](/Cloud/Backups/politica-backups/dp )|


## Bases de datos de tecnología

Orientados a recuperación de desastres.

| Aplicación | Periodicidad de realización del backup | Tiempo retención | Descripción | Ámbito | Detalles |
| --- | --- | --- | --- | --- | --- |
| BTS | Diario | 1 día | Se hace a partir de las 13:00 PM | Tecnología |[Detalles](/Cloud/Backups/politica-backups/tecnologia ) |
| DataSaviour | Diario | 1 día | Se hace a partir de las 13:00 PM (Romance Standard Time)| Tecnología |[Detalles](/Cloud/Backups/politica-backups/tecnologia ) |
| Identity | Diario | 1 día | Se hace a partir de las 13:00 PM (Romance Standard Time) | Tecnología |[Detalles](/Cloud/Backups/politica-backups/tecnologia ) |
| Soyuz | Diario | 1 día | Se hace a partir de las 13:00 PM (Romance Standard Time)| Tecnología |[Detalles](/Cloud/Backups/politica-backups/tecnologia ) |
| salesforce-exchange | Diario | 1 día | Se hace a partir de las 13:00 PM (Romance Standard Time)| Tecnología |[Detalles](/Cloud/Backups/politica-backups/tecnologia ) |
| RoivolutionDistCache | Diario | 1 día | Se hace a partir de las 13:00 PM (Romance Standard Time)| Tecnología |[Detalles](/Cloud/Backups/politica-backups/tecnologia ) |
| Evalutrix | Diario. Pendiente eliminación | 1 día | Se hace a partir de las 13:00 PM (Romance Standard Time)| Tecnología |[Detalles](/Cloud/Backups/politica-backups/tecnologia ) |
| FlowManager | Diario | 1 día | Se hace a las 15:00 PM | Tecnología |[Detalles](/Cloud/Backups/politica-backups/flow ) | 


## Backups de máquinas virtuales

Orientados a recuperación de desastres.

Recovery services vault: rsv-analyticalways-prod
- Si hay que restaurar se hace desde el vault.
- El recovery vault gestiona una cuenta de almacenamiento (staastagingrestorevm)
- Está configurada commo LRS
- Vault soporta tier "Archive" en el storage, pero como solo guardamos 7 días no merece

Hay dos políticas diferentes de backup

### Política: BackupVMFor7DaysPolicy (Standard)

| Aplicación | Periodicidad de realización del backup | Tiempo retención | Descripción | Ámbito | 
| --- | --- | --- | --- | --- | 
| vm-sfera-prod | Diaria | 7 días | Backup completo a las 11 PM (Romance Standard Time) | Clientes | 
| vm-miniso-prod | Diaria | 7 días | Backup completo a las 11 PM (Romance Standard Time) | Clientes | 
| vm-lacoste-prod | Diaria | 7 días | Backup completo a las 11 PM (Romance Standard Time) | Clientes | 
| vm-fbella-prod | Diaria | 7 días | Backup completo a las 11 PM (Romance Standard Time) | Clientes | 
| SRV-ELGANSO | Diaria | 7 días | Backup completo a las 11 PM (Romance Standard Time) | Clientes | 
| vm-csbra-prod | Diaria | 7 días | Backup completo a las 11 PM (Romance Standard Time) | Clientes | 
| vm-csarg-prod | Diaria | 7 días | Backup completo a las 11 PM (Romance Standard Time) | Clientes | 
| SRV-DC03 | Diaria | 7 días | Backup completo a las 11 PM (Romance Standard Time) | IT | 
| SRV-DC04 | Diaria | 7 días | Backup completo a las 11 PM (Romance Standard Time) | IT |

### Política: BackupVmFor7DaysEnhanced (Enhanced)

Recovery services vault: rsv-analyticalways-prod

| Aplicación | Periodicidad de realización del backup | Tiempo retención | Descripción | Ámbito | 
| --- | --- | --- | --- | --- | 
| VM-PRODUCTION | Varios backups al día | 7 días | Desde las 08AM UTC durante 12 horas para dar Instant restore de 2 días. Luego un completo a las 08PM UTC que se guarda 7 días | Clientes |

## Backups de máquinas virtuales de clientes (Hediondos)

Suscripción: DES-Customers (AAHDNDS)
Recovery services vault: rsv-analyticalways-prod
Política: BackupVmFor7DaysPolicy

Full backup todos los días a las 11:30 PM Romance Standard Time
Retención: 7 días

Máquinas:

- vm-intersport (deshabilitado)
- vm-loslocos
- vm-minisochile
- vm-minisocol
- vm-minisoperu
- vm-nikesbay
- vm-pronovias
- vm-tous
- vm-vidal

## Cuentas de storage

Se copian a la subscripción mirror. Para ello se usan runbooks que estan en la Automation account *aa-nostromo-weu*

| Aplicación | Cuenta de storage | Periodicidad |Tiempo retención | Descripción | Ámbito | Detalles |
| --- | --- | --- | --- | --- | --- | --- |
| Pre-season | mpstoragewebjobpro | Diaria, a las 07:00 | --- | --- | Tecnología | [Detalles](/Cloud/Backups/politica-backups/storages ) |
| In-season | securityroistorage | Diaria, a las 07:30 | --- | --- | Tecnología | [Detalles](/Cloud/Backups/politica-backups/storages ) |

## Otros

| Aplicación | Periodicidad de realización del backup | Tiempo retención | Descripción | Ámbito | Detalles |
| --- | --- | --- | --- | --- | --- |
| OMP SQL Server VM's | No se hace backup | -- | --   | Clientes | |
| DataExchange SQL Server VM | No se hace backup | -- | --   | Clientes | |
| Resource center storage | Diaria | 30 días | --   | Marketing | Recovery vault: rsv-analyticalways-prod. Se guardan los shares de esta cuenta de storage: stresourcecenterprod  |

## Backups de Azure DevOps

Se realizan backups de 

- Repositorios
- Pipelines
- Wikis
- Boards
- Artifacts

Más detalles [aquí](/Cloud/Backups/politica-backups/code)

## Backups de Azure App Services

Se realizan cada hora con una retención de 1 mes, usando la configuración automática.

## Backups de Azure Functions

Solo se hace para las azure functions de Pre-season, que comparten un app service plan con el API. 

Se realizan cada hora con una retención de 1 mes, usando la configuración automática.

## Backups de Resource Center

Más detalles [aquí](/Cloud/Backups/politica-backups/resource-center)
