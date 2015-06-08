Um exemplo de como acessar o Azure Blob Storage. 

Possui métodos para fazer Upload, Download e deletar blobs. 

 *É necessário substituir “AccountName” e “AccountKey” pelo nome da sua conta e sua chave, respectivamente.

<h2>Requesitos:</h2>
Visual Studio 2012 + 
Nuget Package

<h2>Descrição</h2>

O Azure Blob Storage é um serviço de armazenamento para dados não-estruturados ( documentos, textos, imagens, videos, etc), que poderão ser acessados de qualquer lugar via HTTP ou HTTPS. Entre os usos mais comuns do Blob Storage estão:

Serviço de imagens ou documentos diretamente para um navegador
Armazenamento de arquivos para acesso distribuído
Streaming de vídeo e áudio
Realização de  backup seguro e recuperação de desastres

Para utilizar o Blob Storage precisamos conhecer 4 conceitos:
<ul>
<li>Account: O acesso ao Azure Storage é feito através de uma conta, onde serão armazenados seus containers e blobs.</li>
<li>Containers: É um recipiente de blobs, podendo conter uma quantia ilimitada de blobs.</li>
<li>Blobs:  É um arquivo de qualquer tipo e tamanho, podendo ser um “page blob” ou “block blob”, veja mais detalhes aqui. </li>
<li>Formato da URL:  O endereço para download dos blobs possui este formato: http://<storage account>. blob.core.windows.net /<container>/<blob></li>
</ul>
