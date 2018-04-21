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
