/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Assets/admin/libs/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assets/admin/libs/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Assets/admin/libs/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Assets/admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Data/Images';
    config.filebrowserFlashUploadUrl = '/Assets/admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    //config.fullPage = false;
    config.allowedContent = true;

    CKFinder.setupCKEditor(null, '/Assets/admin/libs/ckfinder/');
};
