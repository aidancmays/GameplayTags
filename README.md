# Gameplay Tags

Labels for things and actions, as assets: a faction, a damage type, "Exhausted", "Undead". Tags form a tree through
their **Parent**, so *Fire* (parent *Magic*, parent *Damage*) counts as all three.

Used by **Character Stats** (conditional modifiers, pool tags) and **Inventory** (item categories, equipment tags),
and handy on its own.

## Requirements

Unity 6.3 (6000.3) or newer. No other packages.

## Install

Package Manager > **+** > *Add package from disk...* and pick this folder's `package.json`. To edit the package inside a
project, copy the folder into that project's `Packages/` folder instead (an embedded package).

## Use

- **Create > Gameplay Tags > Gameplay Tag** makes a tag. Set its Parent to put it in the tree.
- **TagHolder** (component) holds the tags something has right now. Tags are counted: one added by two sources stays
  until both have removed it. `Add(tag)`, `Remove(tag)`, `Has(tag)` (true for the tag or any descendant), `Held`,
  and a `Changed` event when a tag is gained or lost. `TagHolder.On(component)` finds or adds one.
- `tag.Is(other)` is true when the tag is `other` or one of its descendants.
