class armor {
    [int]$ID
    [string]$name
    [int]$ac
    [int]$cost
    [int]$encumbrance
    [int]$tech_level
}

Remove-Item ./armor.json

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
    'ID'= 3
    'name'="Cuirass, brigandine, linothorax, half-plate"
    'ac'=15
    'cost'=50
    'encumbrance'=1
    'tech_level'=1
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 4
    'name'="Full plate, layered mail"
    'ac'=17
    'cost'=100
    'encumbrance'=2
    'tech_level'=1
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 5
    'name'="Warpaint"
    'ac'=12
    'cost'=300
    'encumbrance'=0
    'tech_level'=4
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 6
    'name'="Armored Undersuit"
    'ac'=13
    'cost'=600
    'encumbrance'=0
    'tech_level'=4
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 7
    'name'="Secure Clothing"
    'ac'=13
    'cost'=300
    'encumbrance'=1
    'tech_level'=4
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 8
    'name'="Armored Vacc Suit"
    'ac'=13
    'cost'=400
    'encumbrance'=2
    'tech_level'=4
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 9
    'name'="Deflector Array"
    'ac'=18
    'cost'=30000
    'encumbrance'=0
    'tech_level'=5
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 10
    'name'="Force Pavis"
    'ac'=15
    'cost'=10000
    'encumbrance'=1
    'tech_level'=5
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 11
    'name'="Security Armor"
    'ac'=14
    'cost'=700
    'encumbrance'=1
    'tech_level'=4
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 12
    'name'="Woven Body Armor"
    'ac'=15
    'cost'=400
    'encumbrance'=2
    'tech_level'=3
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 13
    'name'="Combat Field Uniform"
    'ac'=16
    'cost'=1000
    'encumbrance'=1
    'tech_level'=4
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 14
    'name'="Icarus Harness"
    'ac'=16
    'cost'=8000
    'encumbrance'=1
    'tech_level'=4
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 15
    'name'="Vestimentum"
    'ac'=18
    'cost'=15000
    'encumbrance'=0
    'tech_level'=5
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 16
    'name'="Assault Suit"
    'ac'=18
    'cost'=10000
    'encumbrance'=2
    'tech_level'=4
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 17
    'name'="Storm Armor"
    'ac'=19
    'cost'=20000
    'encumbrance'=2
    'tech_level'=5
}

$collection.Add($new)

$new = New-Object -TypeName armor -Property @{
    'ID'= 1
    'name'="Field Emitter Panoply"
    'ac'=20
    'cost'=40000
    'encumbrance'=1
    'tech_level'=5
}

$collection.Add($new)

$collection | ConvertTo-Json | out-file armor.json