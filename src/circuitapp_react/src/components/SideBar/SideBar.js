import React, {Component} from 'react';
import ShapesGroup from './ShapesGroup';
import SearchInput from './SearchInput';

class SideBar extends Component {
    render() {
        return (
            <div>
                <h2>Shapes</h2>
                <SearchInput />
                <ShapesGroup groupName="Fundamental Items" onAddShape={this.props.onAddShape} />
                <ShapesGroup groupName="Custom Items" />
            </div>
        );
    }
}

export default SideBar;