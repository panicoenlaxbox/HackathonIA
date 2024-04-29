---
title: "Code and other Azure DevOps resources"
date: 2024-03-05
geekdocCollapseSection: true
---

Son bases de datos Sql Azure. 

## ¿Cómo se guardan los backups?


Se realizan mediante una Pipeline de Azure DevOps, que extrae el contenido de este y lo lleva a la suscripción DRP

- Se encuentra en el Proyecto IT de Azure DevOps
- Se llama DR-DevOpsBackups
- Se ejecuta todos los días a las 00:00 UTC


## Dónde se guardan los backups?

Los datos se dejan en la cuenta de storage *staarepoazdevopbackupneu* de la suscripción DRP durante **2 semanas**


![](/Cloud/Backups/politica-backups/images/azdevops.png "Azure DevOps")

El runbook aa-nostromo-neu/Delete-AzureDevops-Containers (suscripción DRP) se encarga de borrar lo antiguo.

Inicialmente se guardan con el tier *hot*. 



TODO: No se está cambiando ese tier
