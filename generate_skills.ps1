class skill {
    [int]$ID
    [string]$Name
    [int]$Level = 0
    
}

$collection = New-Object System.Collections.ArrayList

$new = New-Object -TypeName skill -Property @{
    'ID'= 1
    'Name'="Administer"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 2
    'Name'="Connect"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 3
    'Name'="Exert"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 4
    'Name'="Fix"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 5
    'Name'="Heal"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 6
    'Name'="Know"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 7
    'Name'="Lead"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 8
    'Name'="Notice"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 9
    'Name'="Perform"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 10
    'Name'="Pilot"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 11
    'Name'="Program"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 12
    'Name'="Punch"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 13
    'Name'="Shoot"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 14
    'Name'="Sneak"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 15
    'Name'="Stab"
}

$new = New-Object -TypeName skill -Property @{
    'ID'= 16
    'Name'="Survive"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 17
    'Name'="Talk"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 18
    'Name'="Trade"
}

$collection.Add($new)

$new = New-Object -TypeName skill -Property @{
    'ID'= 19
    'Name'="Work"
}

$collection.Add($new)



$collection | ConvertTo-Json | Out-File skills.json