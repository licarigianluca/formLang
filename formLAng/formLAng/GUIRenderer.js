
var GUIRenderer = function(obj)
{
    var form = $('form');
    var div = $(document.createElement('div'));
    var tag = $(document.createElement(obj.tag)).attr({'id' : obj.id});
    var label = $(document.createElement('label')).attr('for', obj.id).text(obj.label);
    div.attr('hidden', obj.hidden ? 'hidden' : false);
    tag.attr('disabled', obj.readOnly ? 'disabled' : false);

    switch(obj.type)
    {
        case 'integer':
        case 'real':
        case 'money':
            tag.attr('type', 'number');
            if(obj.expr)
            {
                var evalExpr = function() {tag[0].value = eval(obj.expr);};
                setInterval(evalExpr, 1000);
            }
            var updateNumber = function() { window[obj.id] = parseFloat(tag[0].value); };
            updateNumber();
            tag.on('change', updateNumber);
            tag.on('input', updateNumber);
        break;
        case 'boolean':
            tag.attr('type', 'checkbox');
            if(obj.expr)
            {
                var evalExpr = function() {tag[0].checked = eval(obj.expr);};
                setInterval(evalExpr, 100);
            }
            var updateBoolean = function() { window[obj.id] = tag[0].checked; };
            updateBoolean();
            tag.on('change', updateBoolean);
        break;
        case 'date':
            tag.attr('type', 'date');
        break;
        case 'string':
            tag.attr('type', 'text');
            if(obj.expr)
            {
                var evalExpr = function() {tag[0].value = eval(obj.expr);};
                setInterval(evalExpr, 100);
            }
            var updateText = function() { window[obj.id] = tag[0].value; };
            updateText();
            tag.on('change', updateText);
            break;
    }

    if(obj.list)
    {
        for (var key in obj.list)
        {
            var option = $(document.createElement('option')).text(key).attr('value', obj.list[key]);
            tag.append(option);
        }
        tag.on('change', function() {
            var target = $('#'.concat(obj.target));
            target[0].value = this.value;
        target.trigger('change')});
    }
    if(obj.cond)
    {
        setInterval(function()
        {
            try {if(eval(obj.cond))
                div.prop('hidden', false);
            else div.prop('hidden', 'hidden');}
            catch(e){}
        }, 100);
    }
    form.append(div.append(label).append(tag));
};
