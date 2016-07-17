/*******************************************************************************
* KindEditor - WYSIWYG HTML Editor for Internet
* Copyright (C) 2006-2011 kindsoft.net
*
* @author Roddy <luolonghao@gmail.com>
* @site http://www.kindsoft.net/
* @licence http://www.kindsoft.net/license.php
*******************************************************************************/

// google code prettify: http://google-code-prettify.googlecode.com/
// http://google-code-prettify.googlecode.com/

KindEditor.plugin('code', function (K) {
    var self = this, name = 'code';
    self.clickToolbar(name, function () {
        var lang = self.lang(name + '.'),
			html = ['<div style="padding:10px 20px;">',
				'<div class="ke-dialog-row">',
				'<select class="ke-code-type">',
				'<option value="c-sharp">C#</option>',
				'<option value="c-sharp">JavaScript</option>',
				'<option value="c-sharp">HTML</option>',
				'<option value="c-sharp">CSS</option>',
				'<option value="c-sharp">PHP</option>',
				'<option value="c-sharp">Perl</option>',
				'<option value="c-sharp">Python</option>',
				'<option value="c-sharp">Ruby</option>',
				'<option value="c-sharp">Java</option>',
				'<option value="c-sharp">ASP/VB</option>',
				'<option value="c-sharp">C/C++</option>',
				'<option value="c-sharp">XML</option>',
				'<option value="c-sharp">Shell</option>',
				'<option value="c-sharp">Other</option>',
				'</select>',
				'</div>',
				'<textarea class="ke-textarea" style="width:408px;height:260px;"></textarea>',
				'</div>'].join(''),
			dialog = self.createDialog({
			    name: name,
			    width: 450,
			    title: self.lang(name),
			    body: html,
			    yesBtn: {
			        name: self.lang('yes'),
			        click: function (e) {
			            var type = K('.ke-code-type', dialog.div).val(),
							code = textarea.val(),
							cls = type === '' ? 'c-sharp' : type,
							html = '<pre class="brush: ' + cls + '">\n' + K.escape(code) + '</pre> ';
			            if (K.trim(code) === '') {
			                alert(lang.pleaseInput);
			                textarea[0].focus();
			                return;
			            }
			            self.insertHtml(html).hideDialog().focus();
			        }
			    }
			}),
			textarea = K('textarea', dialog.div);
        textarea[0].focus();
    });
});
