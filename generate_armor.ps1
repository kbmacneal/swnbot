class armor {
    [int]$ID
    [string]$name
    [int]$ac
    [int]$cost
    [int]$encumbrance
    [int]$tech_level
}

$collection = New-Object System.Collections.ArrayList

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 2
    'name'="Leather jacks, thick hides, quilted armor"
    'ac'=13
    'cost'=15
    'encumbrance'=1
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Shield"
    'ac'=13
    'cost'=10
    'encumbrance'=2
    'tech_level'=0
}

$collection.Add($new)

$collection | ConvertTo-Json | out-file armor.json