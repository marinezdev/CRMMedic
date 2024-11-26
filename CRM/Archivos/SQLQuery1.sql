SELECT opo.nombre, emp.nombre, opo.Cierre, MONTH(GETDATE()) AS MesActual, MONTH(cierre) AS MesCierre, 
CAST(DAY(GETDATE()) AS VARCHAR) + '/' + CAST(MONTH(GETDATE()) AS VARCHAR)  AS DiaHoy, 
CAST(DAY(cierre) AS VARCHAR)  + '/' + CAST(MONTH(cierre) AS VARCHAR) AS DiaCierre, 
DATEDIFF(d, getdate(), cierre) DiasFaltanAvisos,
CASE 
	WHEN DATEDIFF(d, getdate(), cierre) = 30 THEN 'Primer aviso de proximo evento' 
	WHEN DATEDIFF(d, getdate(), cierre) = 15 THEN 'Segundo aviso de proximo evento'
	ELSE 'Sin novedad'
END AS Mensaje
FROM oportunidades opo 
INNER JOIN oecu ON oecu.IdOportunidad = opo.Id 
INNER JOIN Empresas emp ON emp.Id = oecu.IdEmpresa 
WHERE opo.Estado=1 
AND oecu.idconfiguracion=3 
AND repetirproximoaño=1
--AND MONTH(GETDATE())-1 < MONTH(cierre) AND DAY(GETDATE())+15 < DAY(cierre)  --a 15 días
--AND MONTH(GETDATE()) < MONTH(cierre) 
--AND DATEDIFF(d, GETDATE()+30, cierre) = 30  --a 30 días


