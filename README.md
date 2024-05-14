# DesafioBancoMaster
Desafio técnico

# Rota de Viagem #
Escolha a rota de viagem mais barata independente da quantidade de conexÃµes.
Para isso precisamos inserir as rotas.

# API
## CRUD de cadastro de ROTAS ##
* DeverÃ¡ construir um endpoint de CRUD as rotas disponÃ­veis:
```
Origem: GRU, Destino: BRC, Valor: 10
Origem: BRC, Destino: SCL, Valor: 5
Origem: GRU, Destino: CDG, Valor: 75
Origem: GRU, Destino: SCL, Valor: 20
Origem: GRU, Destino: ORL, Valor: 56
Origem: ORL, Destino: CDG, Valor: 5
Origem: SCL, Destino: ORL, Valor: 20
```

## Explicando ## 
Uma viajem de **GRU** para **CDG** existem as seguintes rotas:

1. GRU - BRC - SCL - ORL - CDG ao custo de $40
2. GRU - ORL - CDG ao custo de $61
3. GRU - CDG ao custo de $75
4. GRU - SCL - ORL - CDG ao custo de $45

O melhor preÃ§o Ã© da rota **1**, apesar de mais conexÃµes, seu valor final Ã© menor.
O resultado da consulta deve ser: **GRU - BRC - SCL - ORL - CDG ao custo de $40**.

Sendo assim, o endpoint de consulta deverÃ¡ efetuar o calculo de melhor rota.


# FRONT-END (Opcional)
Tela para consumir a API (incluir/alterar/excluir)
Tela para consultar melhor rota
* Pode ser apenas uma tela, ou mais, fica a critÃ©rio do desenvolvedor


### Projeto ###
- Interface Front-End (opcional)
	Cadastro: CRUD de Rotas
	Consulta: DeverÃ¡ ter 2 campos para consulta de rota: **Origem-Destino** e exibir o resultado da consulta chamando a API
	
- Interface Rest (ObrigatÃ³rio)
    A interface Rest deverÃ¡ suportar o CRUD de rotas:
    - ManipulaÃ§Ã£o de rotas, dados podendo ser persistidos em arquivo, bd local, etc...
    - Consulta de melhor rota entre dois pontos.
	
  Exemplo:
  ```
  Consulte a rota: GRU-CGD
  Resposta: GRU - BRC - SCL - ORL - CDG ao custo de $40
  
  Consulte a rota: BRC-SCL
  Resposta: BRC - SCL ao custo de $5
  ```


## EntregÃ¡veis ##
* Envie apenas o cÃ³digo fonte
* PreferÃªncia no github ou no OneDrive (zipado)
* Priorize/Estruturar sua aplicaÃ§Ã£o seguindo as boas prÃ¡ticas de desenvolvimento
* Evite o uso de frameworks ou bibliotecas externas Ã  linguagem