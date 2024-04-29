---
title: "Storage accounts"
date: 2024-04-11
geekdocCollapseSection: true
---

## ¿Cómo se guardan los backups?

Se usan runbooks para hacer los backup

## Tenants de Medusa 

- Carpeta **gold/historical/** (Informe de replenishment)

   Se copia después de cada recarga, en el paso del flujo indicado en la imagen (De RoiSyncFiles).
   Storage destino: **streplenishmentbackupneu** (en la suscripción **DRP**)
   Para ello se invovca el runbook **Copy-SpecificReplenishmentReport-ByCustomer** (en la automation account *aa-nostromo-weu*): 

    ![](/Cloud/Backups/politica-backups/images/storages.png "RoiSyncFiles")

- Carpetas **inseason/projections_** e **inseason/prepared_data_for_anual_projection_** (Proyecciones)
   
   Se copian semanalmente, los martes a las 14:00, mediante un runbook aa-nostromo-weu/Backup-Projections-To-North-EuropeV2
   Storage destino: **sttaahistoprojectionsneu** (en la suscripción **DRP**)

- **Storage de merchandise planning**

   Se copia con el runbook MerchandisePlanningStorage (en la automation account *aa-nostromo-weu*)
   Se ejecuta todos los días a las 07:00

   Storage origen: mpstoragewebjobpro
   
   Storage destino: **staamerplanningbackupneu** (en la suscripción **DRP**)

{{< hint warning >}}
🛑 **No se está ejecutando! Hay que revisar**
    
    Además el stotage destino no tiene información
{{< /hint >}}

- **Storage de ROIvolution**

   Se copia con el runbook *Backup-RoivolutionStorage* (en la automation account *aa-nostromo-weu*)
   Se ejecuta todos los días a las 07:30

   Storage origen: securityroistorage
    
   Storage destino: **staaroivolutionbackupneu** (en la suscripción **DRP**)   

{{< hint warning >}}
🛑 **No se está ejecutando! Hay que revisar**

    Además el stotage destino no tiene información
{{< /hint >}}
