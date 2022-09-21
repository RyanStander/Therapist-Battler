#!/bin/zsh
mono /Users/saa/opt/MarkdownGenerator-master/bin/Release/MarkdownGenerator.exe bin/Release/UnityConnector.dll docs
pandoc docs/UnityConnector.md -o docs/UnityConnector_tp.html
sed '/<col.*/d' docs/UnityConnector_tp.html > docs/UnityConnector_tp2.html
{echo -n '<style type="text/css" media="screen">table{border-collapse:collapse;border:1px solid #000000;}table td{border:1px solid #000000;}</style>' ; cat docs/UnityConnector_tp2.html;} > docs/UnityConnector.html
rm docs/UnityConnector_tp*
open docs/UnityConnector.html
