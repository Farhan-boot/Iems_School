// this file is customized by pavel, fixed the wownload data format
//ok: excel.xls,word,csv,txt,sql,xml,json
//bug: powerpoint,ppt,png, pdf 
//todo: for excel download need to see this https://www.travismclarke.com/tableexport/
//for pdf download https://datatables.net/extensions/buttons/examples/html5/simple.html

//http://stackoverflow.com/questions/13405129/javascript-create-and-save-file
function download(data, filename, type) {
    var a = document.createElement("a"),
        file = new Blob([data], { type: type });
    if (window.navigator.msSaveOrOpenBlob) // IE10+
        window.navigator.msSaveOrOpenBlob(file, filename);
    else { // Others
        var url = URL.createObjectURL(file);
        a.href = url;
        a.download = filename;
        document.body.appendChild(a);
        a.click();
        setTimeout(function () {
            document.body.removeChild(a);
            window.URL.revokeObjectURL(url);
        }, 0);
    }
}
//http://ngiriraj.com/pages/htmltable_export/demo.php#
(function ($) {
    $.fn.extend({
        tableExport: function(options) {
            var defaults = {
                separator: ',',
                ignoreColumn: [],
                tableName: 'yourTableName',
                type: 'csv',
                fileName:'ExportedFile',
                pdfFontSize:14,
                pdfLeftMargin:20,
            escape:'true',
            htmlContent:'false',
            consoleLog:'false'
        };
                
    var options = $.extend(defaults, options);
    var el = this;
				
    if(defaults.type == 'csv' || defaults.type == 'txt'){
				
        // Header
        var tdData ="";
        $(el).find('thead').find('tr').each(function() {
            tdData += "\n";					
            $(this).filter(':visible').find('th').each(function(index,data) {
                if ($(this).css('display') != 'none'){
                    if(defaults.ignoreColumn.indexOf(index) == -1){
                        tdData += '"' + parseString($(this)) + '"' + defaults.separator;									
                    }
                }
							
            });
            tdData = $.trim(tdData);
            tdData = $.trim(tdData).substring(0, tdData.length -1);
        });
					
        // Row vs Column
        $(el).find('tbody').find('tr').each(function() {
            tdData += "\n";
            $(this).filter(':visible').find('td').each(function(index,data) {
                if ($(this).css('display') != 'none'){
                    if(defaults.ignoreColumn.indexOf(index) == -1){
                        tdData += '"'+ parseString($(this)) + '"'+ defaults.separator;
                    }
                }
            });
            //tdData = $.trim(tdData);
            tdData = $.trim(tdData).substring(0, tdData.length -1);
        });
					
        //output
        if(defaults.consoleLog == 'true'){
            console.log(tdData);
        }
        //edited by pavel
        //var base64data = "base64," + $.base64.encode(tdData);//note use
        //window.open('data:application/' + defaults.type + ';filename=exportData;' + base64data);// note use

        download(tdData, defaults.fileName + '.' + defaults.type, defaults.type);

    }else if(defaults.type == 'sql'){
				
        // Header
        var tdData ="INSERT INTO '"+defaults.tableName+"' (";
        $(el).find('thead').find('tr').each(function() {
					
            $(this).filter(':visible').find('th').each(function(index,data) {
                if ($(this).css('display') != 'none'){
                    if(defaults.ignoreColumn.indexOf(index) == -1){
                        tdData += "'" + parseString($(this)) + "'," ;									
                    }
                }
							
            });
            tdData = $.trim(tdData);
            tdData = $.trim(tdData).substring(0, tdData.length -1);
        });
        tdData += ") VALUES ";
        // Row vs Column
        $(el).find('tbody').find('tr').each(function() {
            tdData += "(";
            $(this).filter(':visible').find('td').each(function(index,data) {
                if ($(this).css('display') != 'none'){
                    if(defaults.ignoreColumn.indexOf(index) == -1){
                        tdData += "'"+ parseString($(this)) + "';";
                    }
                }
            });
						
            tdData = $.trim(tdData).substring(0, tdData.length -1);
            tdData += "),";
        });
        tdData = $.trim(tdData).substring(0, tdData.length -1);
        tdData += ";";
					
        //output
        //console.log(tdData);
					
        if(defaults.consoleLog == 'true'){
            console.log(tdData);
        }
		//by pavel			
        //var base64data = "base64," + $.base64.encode(tdData);
        //window.open('data:application/sql;filename=exportData;' + base64data);
        download(tdData, defaults.fileName + '.' + defaults.type, defaults.type);//added by pavel
				
    }else if(defaults.type == 'json'){
				
        var jsonHeaderArray = [];
        $(el).find('thead').find('tr').each(function() {
            var tdData ="";	
            var jsonArrayTd = [];
					
            $(this).filter(':visible').find('th').each(function(index,data) {
                if ($(this).css('display') != 'none'){
                    if(defaults.ignoreColumn.indexOf(index) == -1){
                        jsonArrayTd.push(parseString($(this)));									
                    }
                }
            });									
            jsonHeaderArray.push(jsonArrayTd);						
						
        });
					
        var jsonArray = [];
        $(el).find('tbody').find('tr').each(function() {
            var tdData ="";	
            var jsonArrayTd = [];
					
            $(this).filter(':visible').find('td').each(function(index,data) {
                if ($(this).css('display') != 'none'){
                    if(defaults.ignoreColumn.indexOf(index) == -1){
                        jsonArrayTd.push(parseString($(this)));									
                    }
                }
            });									
            jsonArray.push(jsonArrayTd);									
						
        });
					
        var jsonExportArray =[];
        jsonExportArray.push({header:jsonHeaderArray,data:jsonArray});
					
        //Return as JSON
        //console.log(JSON.stringify(jsonExportArray));
					
        //Return as Array
        //console.log(jsonExportArray);
        if(defaults.consoleLog == 'true'){
            console.log(JSON.stringify(jsonExportArray));
        }
        //var base64data = "base64," + $.base64.encode(JSON.stringify(jsonExportArray));
        //window.open('data:application/json;filename=exportData;' + base64data);

        download(JSON.stringify(jsonExportArray), defaults.fileName + '.' + defaults.type, defaults.type);//added by pavel

    }else if(defaults.type == 'xml'){
				
        var xml = '<?xml version="1.0" encoding="utf-8"?>';
        xml += '<tabledata><fields>';

        // Header
        $(el).find('thead').find('tr').each(function() {
            $(this).filter(':visible').find('th').each(function(index,data) {
                if ($(this).css('display') != 'none'){					
                    if(defaults.ignoreColumn.indexOf(index) == -1){
                        xml += "<field>" + parseString($(this)) + "</field>";
                    }
                }
            });									
        });					
        xml += '</fields><data>';
					
        // Row Vs Column
        var rowCount=1;
        $(el).find('tbody').find('tr').each(function() {
            xml += '<row id="'+rowCount+'">';
            var colCount=0;
            $(this).filter(':visible').find('td').each(function(index,data) {
                if ($(this).css('display') != 'none'){	
                    if(defaults.ignoreColumn.indexOf(index) == -1){
                        xml += "<column-"+colCount+">"+parseString($(this))+"</column-"+colCount+">";
                    }
                }
                colCount++;
            });															
            rowCount++;
            xml += '</row>';
        });					
        xml += '</data></tabledata>'
					
        if(defaults.consoleLog == 'true'){
            console.log(xml);
        }
					
        //var base64data = "base64," + $.base64.encode(xml);
        //window.open('data:application/xml;filename=exportData;' + base64data);
        download(xml, defaults.fileName + '.' + defaults.type, defaults.type);

    } else if (defaults.type == 'xls' || defaults.type == 'excel' || defaults.type == 'docx' || defaults.type == 'doc' ||defaults.type == 'ppt'|| defaults.type == 'powerpoint') {
        //console.log($(this).html());

        var excel ="<table  cellpadding='0' cellspacing='0'>";
        // Header
        $(el).find('tr').each(function() {
            var rowStyle = $(this).prop("style");
            excel += "<tr style='" + rowStyle.cssText + "'>";
            $(this).filter(':visible').find('th,td').each(function (index, data) {
                if (!$(this).hasClass("hidden-print"))
                if ($(this).css('display') != 'none') {
                    if(defaults.ignoreColumn.indexOf(index) == -1){

                        var colspan = $(this).prop("colspan");
                        var rowspan = $(this).prop("rowspan");
                        var style = $(this).prop("style");
                        var colspanText = "";
                        var rowspanText = "";

                        if (colspan > 1) {
                            colspanText = "colspan = '" + colspan + "'";
                        }
                        if (rowspan > 1) {
                            rowspanText = "rowspan = '" + rowspan + "'";
                        }

                        excel += "<td style='border: 0.5pt solid windowtext;text-align:center;" + style.cssText + "' " + colspanText + " " + rowspanText + ">" + parseString($(this)) + "</td>";
                    }
                }
            });	
            excel += '</tr>';						
						
        });

        //// Row Vs Column
        //var rowCount=1;
        //$(el).find('tbody').find('tr').each(function () {
        //    var rowStyle = $(this).prop("style");
        //    excel += "<tr style='"+rowStyle.cssText+"'>";
        //    var colCount=0;
        //    $(this).filter(':visible').find('td').each(function (index, data) {
        //        if (!$(this).hasClass("hidden-print"))
        //        if ($(this).css('display') != 'none'){	
        //            if(defaults.ignoreColumn.indexOf(index) == -1){
        //                var colspan = $(this).prop("colspan");
        //                var rowspan = $(this).prop("rowspan");
        //                var style = $(this).prop("style");
        //                var colspanText = "";
        //                var rowspanText = "";

        //                if (colspan > 1) {
        //                    colspanText = "colspan = '" + colspan + "'";
        //                }
        //                if (rowspan > 1) {
        //                    rowspanText = "rowspan = '" + rowspan + "'";
        //                }

        //                excel += "<td style='border: 0.5pt solid windowtext;text-align:center;" + style.cssText+"' " + colspanText + " " + rowspanText + ">" + parseString($(this)) + "</td>";
        //            }
        //        }
        //        colCount++;
        //    });															
        //    rowCount++;
        //    excel += '</tr>';
        //});					
        excel += '</table>'
					
        if(defaults.consoleLog == 'true'){
            console.log(excel);
        }
					
        var excelFile = "<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:"+defaults.type+"' xmlns='http://www.w3.org/TR/REC-html40'>";
        excelFile += "<head>";
        excelFile += "<!--[if gte mso 9]>";
        excelFile += "<xml>";
        excelFile += "<x:ExcelWorkbook>";
        excelFile += "<x:ExcelWorksheets>";
        excelFile += "<x:ExcelWorksheet>";
        excelFile += "<x:Name>";
        excelFile += "{worksheet}";
        excelFile += "</x:Name>";
        excelFile += "<x:WorksheetOptions>";
        excelFile += "<x:DisplayGridlines/>";
        excelFile += "</x:WorksheetOptions>";
        excelFile += "</x:ExcelWorksheet>";
        excelFile += "</x:ExcelWorksheets>";
        excelFile += "</x:ExcelWorkbook>";
        excelFile += "</xml>";
        excelFile += "<![endif]-->";
        excelFile += "</head>";
        excelFile += "<body>";
        excelFile += excel;
        excelFile += "</body>";
        excelFile += "</html>";

        //by pavel
        //var base64data = "base64," + $.base64.encode(excelFile);
        //if (defaults.type == 'xls' || defaults.type == 'excel')
        //    window.open('data:application/vnd.ms-excel' + ';filename=exportData.doc;' + base64data);
        //else if (defaults.type == 'ppt' || defaults.type == 'powerpoint')
        //    window.open('data:application/vnd.ms-powerpoint' + ';filename=exportData.doc;' + base64data);
        //else
        if (defaults.type == 'xls' || defaults.type == 'excel')
            defaults.type = 'xls';
        download(excelFile, defaults.fileName + '.' + defaults.type, defaults.type);//for doc
					
    }else if(defaults.type == 'png'){
        html2canvas($(el), {
            onrendered: function(canvas) {										
                var img = canvas.toDataURL("image/png");
                window.open(img);
							
							
            }
        });		
    }else if(defaults.type == 'pdf'){
	
        var doc = new jsPDF('p','pt', 'a4', true);
        doc.setFontSize(defaults.pdfFontSize);
					
        // Header
        var startColPosition=defaults.pdfLeftMargin;
        $(el).find('thead').find('tr').each(function() {
            $(this).filter(':visible').find('th').each(function(index,data) {
                if ($(this).css('display') != 'none'){					
                    if(defaults.ignoreColumn.indexOf(index) == -1){
                        var colPosition = startColPosition+ (index * 50);									
                        doc.text(colPosition,20, parseString($(this)));
                    }
                }
            });									
        });					
				
				
        // Row Vs Column
        var startRowPosition = 20; var page =1;var rowPosition=0;
        $(el).find('tbody').find('tr').each(function(index,data) {
            rowCalc = index+1;
						
            if (rowCalc % 26 == 0){
                doc.addPage();
                page++;
                startRowPosition=startRowPosition+10;
            }
            rowPosition=(startRowPosition + (rowCalc * 10)) - ((page -1) * 280);
						
            $(this).filter(':visible').find('td').each(function(index,data) {
                if ($(this).css('display') != 'none'){	
                    if(defaults.ignoreColumn.indexOf(index) == -1){
                        var colPosition = startColPosition+ (index * 50);									
                        doc.text(colPosition,rowPosition, parseString($(this)));
                    }
                }
							
            });															
						
        });					
										
        // Output as Data URI
        doc.output('datauri');
	
    }
				
				
    function parseString(data){
				
        if(defaults.htmlContent == 'true'){
            content_data = data.html().trim();
        }else{
            content_data = data.text().trim();
        }
					
        if(defaults.escape == 'true'){
            content_data = escape(content_data);
        }
        //getting text from input box,added by pavel
        var inputValue = $(data).find('*').filter(':input:visible:first');
        if (inputValue != undefined && inputValue.val()!= undefined) {
           // console.log(": " + inputValue.val());
            content_data = (content_data + ' ' + inputValue.val()).trim();
        }
	
        return content_data;
    }
			
}
});
})(jQuery);
        