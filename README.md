# DSTris
Projecto para a Unidade Curricular de Laboratório de Desenvolvimento de Software

Clone do Tetris 2D usando a biblioteca [SFML.Net](https://www.sfml-dev.org/download/sfml.net/)

### Pré-requisitos

É necessario fazer download do SFML.Net.

### Instalar

* Adicionar referência aos ficheiros sfmlnet-audio-2.dll, sfmlnet-graphics-2.dll, sfmlnet-system-2.dll e sfmlnet-window-2.dll, disponíveis na pasta lib do SFML.Net.
* Adicionar referência à assembly System.Xml
* Copiar os ficheiros csfml-audio-2.dll, csfml-graphics-2.dll, csfml-network-2.dll, csfml-system-2.dlle csfml-window-2.dll, disponíveis na pasta extlibs para a pasta do projecto.
* Incluir os ficheiros csfm* no projecto para facilitar a distribuição.
* Alterar a propriedade "Copy to Output Directory" para "Copy if newer", de modo a que os ficheiros sejam copiados para a pasta onde é gerada a build.

### Pontos por implementar

* Estrutura
  * [X] Criar pasta para os assets, contem a fonte, imagens e sons
  * [X] Mostrar imagens de fundo nos vários estados (menu e em jogo)
  * [X] Ler restantes dados da configuração 
  * [X] Gerar bloco em jogo e proximo bloco
  * [X] Guardar grelha com peças que já pousaram
  * [X] Desenhar peça atual, proxima e grelha de jogo
* Jogabilidade
  * [X] Ler input do utilizador para mover e rodar as peças
  * [X] Validar posição
  * [X] Quando houver colisão na descida, pousar a peça
  * [X] Verificar se cria linha
  * [X] Limpar linhas e mover peças em posição superior
  * [X] Colocar o jogo em pausa e retomar
  * [ ] Contabilizar pontos
  * [ ] Mostrar estatistica das peças que sairam
  * [ ] Criar niveis (aumentando a dificuldade)
* Testes
  * [X] Validar existencia dos ficheiros lidos (configuração, texturas e fonte)
  * [X] Mostrar estado atual do jogo na consola
  * [X] Adicionar contador de frames por segundo, para ter noção se tem velocidade aceitavel
  * [X] Validar publicação para confirmar que todos os ficheiros estão a ser distribuidos
  * [X] Ao mover as peças, não sobrepor as existentes na grelha do jogo
  * [X] Não mover peças para fora da área de jogo
  * [X] Quando gerar nova peça, se não conseguir colocar na grelha dar jogo como terminado
	